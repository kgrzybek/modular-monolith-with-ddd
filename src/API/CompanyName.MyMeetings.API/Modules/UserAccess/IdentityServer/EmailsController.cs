using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.IdentityServer
{
    [Route("api/userAccess/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public EmailsController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [NoPermissionRequired]
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetEmails()
        {
            var allEmails = await _userAccessModule.ExecuteQueryAsync(new GetAllEmailsQuery());

            return Ok(allEmails);
        }
    }
}