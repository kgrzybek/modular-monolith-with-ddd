using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups;
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
        public async Task<IActionResult> GetAuthenticatedMemberMeetingGroups()
        {
            var meetingGroups = await _meetingsModule.ExecuteQueryAsync(
                new GetAuthenticationMemberMeetingGroupsQuery());

            return Ok(meetingGroups);
        }

        [HttpGet("all")]
        [HasPermission(MeetingsPermissions.GetAllMeetingGroups)]
        public async Task<IActionResult> GetAllMeetingGroups()
        {
            var meetingGroups = await _meetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupsQuery());

            return Ok(meetingGroups);
        }

        [HttpPut("{meetingGroupId}")]
        [HasPermission(MeetingsPermissions.EditMeetingGroupGeneralAttributes)]
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
        public async Task<IActionResult> JoinToGroup(Guid meetingGroupId)
        {
            await _meetingsModule.ExecuteCommandAsync(new JoinToGroupCommand(meetingGroupId));

            return Ok();
        }

        [HttpDelete("{meetingGroupId}/members")]
        [HasPermission(MeetingsPermissions.LeaveMeetingGroup)]
        public async Task<IActionResult> LeaveMeetingGroup(Guid meetingGroupId)
        {
            await _meetingsModule.ExecuteCommandAsync(new LeaveMeetingGroupCommand(meetingGroupId));

            return Ok();
        }
    }
}