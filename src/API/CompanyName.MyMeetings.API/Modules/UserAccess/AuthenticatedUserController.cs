using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetAuthenticatedUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess
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
        [HttpGet("permissions")]
        public async Task<IActionResult> GetAuthenticatedUserPermissions()
        {
            var permissions = await _userAccessModule.ExecuteQueryAsync(new GetAuthenticatedUserPermissionsQuery());

            return Ok(permissions);
        }
    }
}