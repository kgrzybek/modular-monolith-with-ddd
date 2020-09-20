using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.DisbaleMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfiguration.EnableMeetingCommentingConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingCommentingConfiguration
{
    [Route("api/meetings/meetings/{meetingId}/configuration/commenting")]
    [ApiController]
    public class MeetingCommentingConfigurationController : ControllerBase
    {
        private readonly IMeetingsModule _meetingsModule;

        public MeetingCommentingConfigurationController(IMeetingsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpPatchAttribute("disable")]
        [HasPermission(MeetingsPermissions.EditMeetingCommentingConfiguration)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisableCommenting(Guid meetingId)
        {
            await _meetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(meetingId));
            return Ok();
        }

        [HttpPatchAttribute("enable")]
        [HasPermission(MeetingsPermissions.EditMeetingCommentingConfiguration)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EnableCommenting(Guid meetingId)
        {
            await _meetingsModule.ExecuteCommandAsync(new EnableMeetingCommentingConfigurationCommand(meetingId));
            return Ok();
        }
    }
}