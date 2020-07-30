using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingGroupProposals
{
    [Route("api/meetings/[controller]")]
    [ApiController]
    public class MeetingGroupProposalsController : ControllerBase
    {
        private readonly IMeetingsModule _meetingsModule;

        public MeetingGroupProposalsController(IMeetingsModule meetingsModule)
        {
            _meetingsModule = meetingsModule;
        }

        [HttpGet("")]
        [HasPermission(MeetingsPermissions.GetMeetingGroupProposals)]
        public async Task<IActionResult> GetMeetingGroupProposals(int? page, int? perPage)
        {
            var meetingGroupProposals = await _meetingsModule.ExecuteQueryAsync(
                new GetMeetingGroupProposalsQuery(page, perPage));

            return Ok(meetingGroupProposals);
        }

        [HttpPost("")]
        [HasPermission(MeetingsPermissions.ProposeMeetingGroup)]
        public async Task<IActionResult> ProposeMeetingGroup(ProposeMeetingGroupRequest request)
        {
            await _meetingsModule.ExecuteCommandAsync(
                new ProposeMeetingGroupCommand(
                    request.Name,
                    request.Description,
                    request.LocationCity,
                    request.LocationCountryCode));

            return Ok();
        }
    }
}