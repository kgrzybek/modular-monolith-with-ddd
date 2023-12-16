using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.LeaveMeetingGroup;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingGroups
{
    [Route("api/meetings/[controller]")]
    [ApiController]
    public class MeetingGroupsController : ControllerBase
    {
        private readonly IMeetingsModule _meetingsModule;

        public MeetingGroupsController(IMeetingsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpGet("")]
        [HasPermission(MeetingsPermissions.GetAuthenticatedMemberMeetingGroups)]
        [ProducesResponseType(typeof(List<MemberMeetingGroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthenticatedMemberMeetingGroups()
        {
            var meetingGroups = await _meetingsModule.ExecuteQueryAsync(
                new GetAuthenticationMemberMeetingGroupsQuery());

            return Ok(meetingGroups);
        }

        [HttpGet("{meetingGroupId}")]
        [HasPermission(MeetingsPermissions.GetMeetingGroupDetails)]
        [ProducesResponseType(typeof(MeetingGroupDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMeetingGroupDetails(Guid meetingGroupId)
        {
            var meetingGroupDetails = await _meetingsModule.ExecuteQueryAsync(
                new GetMeetingGroupDetailsQuery(meetingGroupId));

            return Ok(meetingGroupDetails);
        }

        [HttpGet("all")]
        [HasPermission(MeetingsPermissions.GetAllMeetingGroups)]
        [ProducesResponseType(typeof(List<MeetingGroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMeetingGroups()
        {
            var meetingGroups = await _meetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupsQuery());

            return Ok(meetingGroups);
        }

        [HttpPut("{meetingGroupId}")]
        [HasPermission(MeetingsPermissions.EditMeetingGroupGeneralAttributes)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditMeetingGroupGeneralAttributes(
            [FromRoute] Guid meetingGroupId,
            [FromBody] EditMeetingGroupGeneralAttributesRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(new EditMeetingGroupGeneralAttributesCommand(
                meetingGroupId,
                request.Name,
                request.Description,
                request.LocationCity,
                request.LocationCountry));

            return Ok();
        }

        [HttpPost("{meetingGroupId}/members")]
        [HasPermission(MeetingsPermissions.JoinToGroup)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinToGroup(Guid meetingGroupId)
        {
            await _meetingsModule.ExecuteCommandAsync(new JoinToGroupCommand(meetingGroupId));

            return Ok();
        }

        [HttpDelete("{meetingGroupId}/members")]
        [HasPermission(MeetingsPermissions.LeaveMeetingGroup)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LeaveMeetingGroup(Guid meetingGroupId)
        {
            await _meetingsModule.ExecuteCommandAsync(new LeaveMeetingGroupCommand(meetingGroupId));

            return Ok();
        }
    }
}