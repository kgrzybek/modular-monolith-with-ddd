using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Authorization.GetAuthenticatedUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Authorization.GetUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Users.GetAuthenticatedUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Users.GetUser;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.IdentityServer
{
    [Route("api/userAccess/authenticatedUser")]
    [ApiController]
    public class AuthenticatedUserController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public AuthenticatedUserController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [NoPermissionRequired]
        [HttpGet("")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            var user = await _userAccessModule.ExecuteQueryAsync(new GetAuthenticatedUserQuery());

            return Ok(user);
        }

        [NoPermissionRequired]
        [HttpGet("permissions")]
        [ProducesResponseType(typeof(List<UserPermissionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthenticatedUserPermissions()
        {
            var permissions = await _userAccessModule.ExecuteQueryAsync(new GetAuthenticatedUserPermissionsQuery());

            return Ok(permissions);
        }
    }
}