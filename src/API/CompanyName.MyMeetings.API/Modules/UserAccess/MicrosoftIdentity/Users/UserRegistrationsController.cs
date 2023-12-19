using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.Contracts.V1.Users.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.RegisterNewUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Users;

[Route("userAccess/[controller]")]
[ApiExplorerSettings(GroupName = "User Access")]
public class UserRegistrationsController : ApplicationController
{
    private readonly IUserAccessModule _userAccessModule;

    public UserRegistrationsController(IUserAccessModule userAccessModule)
    {
        _userAccessModule = userAccessModule;
    }

    [NoPermissionRequired]
    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
    {
        var result = await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
            request.Login,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName,
            request.ConfirmLink));

        return result.ToApiResult();
    }

    [NoPermissionRequired]
    [AllowAnonymous]
    [HttpPatch("{userRegistrationId}/confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
    {
        var result = await _userAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));
        return result.ToApiResult();
    }
}