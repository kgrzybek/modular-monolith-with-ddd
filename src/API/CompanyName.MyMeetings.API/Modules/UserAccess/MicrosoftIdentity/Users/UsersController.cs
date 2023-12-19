using AutoMapper;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.ByUserId;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetUserRoles;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.GetAuthenticatorKey;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.RegisterAuthenticator;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ConfirmEmailAddress;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.CreateUserAccount;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserRoles;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.UnlockUserAccount;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.UpdateUserAccount;
using Microsoft.AspNetCore.Mvc;
using UserContracts = CompanyName.MyMeetings.Contracts.V1.Users.Users;
using UsersApplication = CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.GetUserAccounts;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Users;

[Route("api/userAccess/Users")]
[ApiExplorerSettings(GroupName = "User Access")]
public class UsersController : ApplicationController
{
    private readonly IMapper _mapper;
    private readonly IUserAccessModule _userAccessModule;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public UsersController(IUserAccessModule userAccessModule, IExecutionContextAccessor executionContextAccessor, IMapper mapper)
    {
        _mapper = mapper;
        _userAccessModule = userAccessModule;
        _executionContextAccessor = executionContextAccessor;
    }

    /// <summary>
    /// Gets the user directory.
    /// </summary>
    /// <returns>List of users.</returns>
    [HttpGet]
    [HasPermission(UsersPermissions.GetUsers)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserAccountDirectory()
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new UsersApplication.Directory.GetUserAccountsQuery());
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var users = _mapper.Map<IEnumerable<UserContracts.UserAccountDto>>(response.Value);
        return response.ToApiResult(users);
    }

    [HttpGet("{userId}")]
    [HasPermission(UsersPermissions.GetUsers)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserAccount(Guid userId)
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new UsersApplication.ById.GetUserAccountsQuery(userId));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var user = _mapper.Map<UserContracts.UserAccountDto>(response.Value);
        return response.ToApiResult(user);
    }

    [HttpPost]
    [HasPermission(UsersPermissions.CreateUserAccount)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateUserAccount(UserContracts.CreateUserAccountRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(
            new CreateUserAccountCommand(
                request.UserName, request.Password, request.Name, request.FirstName, request.LastName, request.EmailAddress));

        return response.ToApiResult();
    }

    [HttpPut("{userId}")]
    [HasPermission(UsersPermissions.UpdateUserAccount)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateUserAccount(Guid userId, UserContracts.UpdateUserAccountRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new UpdateUserAccountCommand(userId, request.Name, request.FirstName, request.LastName));
        return response.ToApiResult();
    }

    [HttpPatch("{userId}/unlock")]
    [HasPermission(UsersPermissions.UnlockUserAccount)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UnlockUserAccount(Guid userId)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new UnlockUserAccountCommand(userId));
        return response.ToApiResult();
    }

    [HttpPost("confirm-email-address")]
    [HasPermission(UsersPermissions.ConfirmEmailAddress)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ConfirmEmailAddress(UserContracts.ConfirmEmailAddressRequest confirmEmailAddress)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new ConfirmEmailAddressCommand(confirmEmailAddress.EmailAddress, confirmEmailAddress.Token));
        return response.ToApiResult();
    }

    /// <summary>
    /// Get the authenticator key for the current logged in user.
    /// Use this key to register an new account in an authenticator app.
    /// </summary>
    /// <returns>The authenticator key.</returns>
    [HttpGet("authenticator-key")]
    [HasPermission(UsersPermissions.GetAuthenticatorKey)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAuthenticatorKey()
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new GetAuthenticatorKeyQuery(_executionContextAccessor.UserId));
        return response.ToApiResult();
    }

    /// <summary>
    /// Enable two-step authentication for the current logged in user by providing the code generated by the authenticator app.
    /// </summary>
    /// <param name="otpCode">One-Time Password Code.</param>
    /// <returns>Result.</returns>
    [HttpPost("register-authenticator")]
    [HasPermission(UsersPermissions.RegisterAuthenticator)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RegisterAuthenticator(string otpCode)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new RegisterAuthenticatorCommand(_executionContextAccessor.UserId, otpCode));
        return response.ToApiResult();
    }

    [HttpGet("{userId}/roles")]
    [HasPermission(UsersPermissions.GetUserRoles)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserRoles(Guid userId)
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new GetUserRolesQuery(userId));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var roles = _mapper.Map<IEnumerable<UserContracts.RoleDto>>(response.Value);
        return response.ToApiResult(roles);
    }

    [HttpPatch("{userId}/roles")]
    [HasPermission(UsersPermissions.SetUserRoles)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetUserRoles(Guid userId, UserContracts.SetUserRolesRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new SetUserRolesCommand(userId, request.RoleIds));
        return response.ToApiResult();
    }

    [HttpGet("{userId}/permissions")]
    [HasPermission(UsersPermissions.GetUserPermissions)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserPermissions(Guid userId)
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new GetPermissionsQuery(userId));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var permissions = _mapper.Map<IEnumerable<UserContracts.PermissionDto>>(response.Value);
        return response.ToApiResult(permissions);
    }

    [HttpPatch("{userId}/permissions")]
    [HasPermission(UsersPermissions.SetUserPermissions)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetUserPermissions(Guid userId, [FromBody] UserContracts.SetUserPermissionsRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new SetUserPermissionsCommand(userId, request.Permissions));
        return response.ToApiResult();
    }
}