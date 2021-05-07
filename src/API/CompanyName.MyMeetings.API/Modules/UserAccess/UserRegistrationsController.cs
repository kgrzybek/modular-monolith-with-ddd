using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess
{
    [Route("userAccess/[controller]")]
    [ApiController]
    public class UserRegistrationsController : ControllerBase
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
            await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                request.Login,
                request.Password,
                request.Email,
                request.FirstName,
                request.LastName,
                request.ConfirmLink));

            return Ok();
        }

        [NoPermissionRequired]
        [AllowAnonymous]
        [HttpPatch("{userRegistrationId}/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
        {
            await _userAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            return Ok();
        }
    }
}