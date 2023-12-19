using System.Security.Claims;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

internal class AccountLoginCommandHandler : ICommandHandler<AccountLoginCommand, AuthenticationResult>
{
    private readonly IEmailSender _emailSender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityOptions _identityOptions;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public AccountLoginCommandHandler(
        IEmailSender emailSender,
        UserManager<ApplicationUser> userManager,
        ITokenClaimsService tokenClaimsService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _emailSender = emailSender;
        _userManager = userManager;
        _identityOptions = _userManager.Options;
        _tokenClaimsService = tokenClaimsService;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<AuthenticationResult> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
    {
        var response = new AuthenticationResult();

        var user = await _userManager.FindByNameAsync(request.Login);
        if (user != null && !await _userManager.IsLockedOutAsync(user))
        {
            if (await _userManager.HasPasswordAsync(user))
            {
                var passwordCheckSucceeded = await _userManager.CheckPasswordAsync(user, request.Password);
                if (passwordCheckSucceeded)
                {
                    // Reset failed account login attempts
                    await _userManager.ResetAccessFailedCountAsync(user);

                    // Check if user is allowed to login
                    if (_identityOptions.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(user))
                    {
                        if (!string.IsNullOrEmpty(user.Email))
                        {
                            var emailMessage = new EmailMessage(
                                user.Email!,
                                "MyMeetings user account not validated!",
                                $@"You cannot log in with this user account because the e-mail address you have entered has not yet been validated.\n\nUser name: {user.UserName}\nEmail: {user.Email}");

                            await _emailSender.SendEmail(emailMessage);
                        }

                        response.AddError(Errors.UserAccess.EmailNotConfirmed);
                    }
                    else
                    {
                        if (await _userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validProviders = await _userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validProviders.Contains(_userManager.Options.Tokens.AuthenticatorTokenProvider))
                            {
                                response.StoreTwoFactorAuthentication(Generate2FA(user.Id, _userManager.Options.Tokens.AuthenticatorTokenProvider));
                            }
                            else if (validProviders.Contains("Email"))
                            {
                                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                                var emailMessage = new EmailMessage(
                                    user.Email,
                                    "MyMeetings Token",
                                    "Here is your token which you need for the registration.\n\nToken: {token}");

                                await _emailSender.SendEmail(emailMessage);

                                response.StoreTwoFactorAuthentication(Generate2FA(user.Id, "Email"));
                            }
                        }
                        else
                        {
                            var userDto = new UserDto()
                            {
                                Id = user.Id,
                                Name = $"{user.Name}".Trim(),
                                UserName = user.UserName,
                                Email = user.Email,
                                Claims = _tokenClaimsService.GetUserClaims(user)
                            };

                            var tokens = _tokenClaimsService.GenerateTokens(user);
                            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                            response.SetAuthenticatedUser(userDto, tokens.AccessToken, tokens.RefreshToken, principal);
                        }
                    }
                }
                else
                {
                    // Increase the failed account login count
                    await _userManager.AccessFailedAsync(user);
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        await SendNotificationEmailAsync(user);
                    }

                    response.AddError(Errors.UserAccess.InvalidUserNameOrPassword);
                }
            }
            else
            {
                var emailMessage = new EmailMessage(
                        user.Email,
                        "MyMeetings user account not enabled!",
                        $"You cannot log in with this user account because no password has been defined for this account.\n\nUser name: {user.UserName}\nEmail: {user.Email}");

                await _emailSender.SendEmail(emailMessage);

                response.AddError(Errors.UserAccess.LoginNotAllowed);
            }
        }
        else
        {
            // Instead of returning an "User not found." error message return an more generic one.
            response.AddError(Errors.UserAccess.InvalidUserNameOrPassword);
        }

        return response;
    }

    private static ClaimsPrincipal Generate2FA(Guid userId, string provider)
    {
        var identity = new ClaimsIdentity(
            new List<Claim>
            {
                new Claim("sub", userId.ToString()),
                new Claim("amr", provider) // Authentication method reference
            },
            IdentityConstants.TwoFactorUserIdScheme);

        return new ClaimsPrincipal(identity);
    }

    private async Task SendNotificationEmailAsync(ApplicationUser user)
    {
        if (!string.IsNullOrEmpty(user.Email))
        {
            var emailMessage = new EmailMessage(
                user.Email,
                "MyMeetings user account locked!",
                $"Your user account has been blocked for security reasons.\n\nUser name: {user.UserName}\n\nThe account has been locked for {_identityOptions.Lockout.DefaultLockoutTimeSpan.Minutes}minutes because the password was entered incorrectly {_identityOptions.Lockout.MaxFailedAccessAttempts} times in a row. After this time, the lock is automatically removed.\n\nIf you are not responsible for locking the user account, please contact the administrator.");

            await _emailSender.SendEmail(emailMessage);
        }
    }
}