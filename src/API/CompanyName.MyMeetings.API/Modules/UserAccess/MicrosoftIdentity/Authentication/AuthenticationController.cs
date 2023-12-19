using System.Security.Claims;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Contracts.V1.Users.Authentication;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login.External;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RefreshToken;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RequestForgotPasswordLink;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.ResetPassword;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authentication;

[AllowAnonymous]
[Route("api/userAccess/authentication")]
[ApiExplorerSettings(GroupName = "Authentication")]
public class AuthenticationController : ApplicationController
{
    private readonly IUserAccessModule _userAccessModule;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthenticationController(IUserAccessModule userAccessModule, IExecutionContextAccessor executionContextAccessor)
    {
        _userAccessModule = userAccessModule;
        _executionContextAccessor = executionContextAccessor;
    }

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="request">Authentication attributes.</param>
    /// <returns>ApiResult.</returns>
    [HttpPost("login")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(AuthenticationRequest request)
    {
        AuthenticationResultDto result = null;

        var response = await _userAccessModule.ExecuteCommandAsync(new AccountLoginCommand(request.UserName, request.Password));
        if (response != null)
        {
            if (response.RequiresTwoFactor)
            {
                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, response.ClaimsPrincipal);

                result = new AuthenticationResultDto()
                {
                    RequiresTwoFactor = true
                };
            }
            else if (response.IsAuthenticated)
            {
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, response.ClaimsPrincipal);

                result = new AuthenticationResultDto()
                {
                    UserName = response.User.UserName,
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken
                };
            }

            return response.ToApiResult(result);
        }

        return Error(Errors.General.InvalidRequest());
    }

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="token">User generated token.</param>
    /// <returns>ApiResult.</returns>
    [HttpPost("two-factor-login")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TwoFactorLogin(string token)
    {
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
        if (result != null)
        {
            if (!result.Succeeded)
            {
                return Error(Errors.Authentication.LoginRequestExpired());
            }

            var response = await _userAccessModule.ExecuteCommandAsync(new AccountTwoFactorLoginCommand(
                    Guid.Parse(result.Principal.FindFirstValue("sub")),
                    result.Principal.FindFirstValue("amr"),
                    token));

            if (response != null)
            {
                if (response.IsAuthenticated)
                {
                    // Clean up the cookie
                    await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, response.ClaimsPrincipal);

                    AuthenticationResultDto authenticationResult = new AuthenticationResultDto()
                    {
                        UserName = response.User.UserName,
                        AccessToken = response.AccessToken,
                        RefreshToken = response.RefreshToken
                    };
                    return Ok(authenticationResult);
                }

                return Error(Errors.Authentication.InvalidToken());
            }
        }

        return Error(Errors.General.InvalidRequest());
    }

    /// <summary>
    /// Send forgot password link.
    /// </summary>
    /// <param name="emailAddress">Email address of the user.</param>
    /// <returns>ApiResult.</returns>
    [HttpPost("request-forgot-password-link")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestForgotPasswordLink(string emailAddress)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new RequestForgotPasswordLinkCommand(emailAddress));
        return response.ToApiResult(response.Token);
    }

    /// <summary>
    /// Reset password.
    /// </summary>
    /// <param name="resetPassword">Reset password attributes.</param>
    /// <returns>ApiResult.</returns>
    [HttpPost("reset-password")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPassword)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new ResetPasswordCommand(resetPassword.Token, resetPassword.EmailAddress, resetPassword.Password));
        return response.ToApiResult();
    }

    [HttpGet("external-login")]
    [NoPermissionRequired]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult ExternalLogin(string provider)
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(ExternalLoginCallback)),
            Items = { { "scheme", provider } }
        };
        return Challenge(properties, provider);
    }

    [HttpGet("external-login-callback")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        if (result != null)
        {
            // We really need the external user identifier
            var externalUserId = result.Principal.FindFirstValue("sub")
                ?? result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Cannot find external user id");

            // Get the provider from the authentication properties which is available from the scheme item
            var provider = result.Properties.Items["scheme"];

            var emailAddress = result.Principal.FindFirstValue("email")
                ?? result.Principal.FindFirstValue(ClaimTypes.Email);

            // Once we have all this we can go ahead an call the external login command
            var response = await _userAccessModule.ExecuteCommandAsync(new ExternalAccountLoginCommand(provider, externalUserId, emailAddress, false));

            if (response != null)
            {
                if (response.IsAuthenticated)
                {
                    // Clean up the cookie
                    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, response.ClaimsPrincipal);

                    var authenticationResult = new AuthenticationResultDto()
                    {
                        UserName = response.User.UserName,
                        AccessToken = response.AccessToken,
                        RefreshToken = response.RefreshToken
                    };
                    return response.ToApiResult(authenticationResult);
                }

                return Error(Errors.Authentication.InvalidToken());
            }
        }

        return Error(Errors.General.InvalidRequest());
    }

    [HttpPost("refresh-token")]
    [NoPermissionRequired]
    public async Task<IActionResult> RefreshToken(TokenRequest tokenRequest)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new RefreshTokenCommand(tokenRequest.AccessToken, tokenRequest.RefreshToken));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var tokenResult = new TokenResultDto()
        {
            AccessToken = response.Value!.AccessToken,
            RefreshToken = response.Value!.RefreshToken
        };
        return response.ToApiResult(tokenResult);
    }
}