using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration;
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

        [HttpPatch("disable")]
        [HasPermission(MeetingsPermissions.DisableMeetingCommenting)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DisableCommenting(Guid meetingId)
        {
            await _meetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(meetingId));
            return Ok();
        }

        [HttpPatch("enable")]
        [HasPermission(MeetingsPermissions.EnableMeetingCommenting)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EnableCommenting(Guid meetingId)
        {
            await _meetingsModule.ExecuteCommandAsync(new EnableMeetingCommentingConfigurationCommand(meetingId));
            return Ok();
        }
    }
}