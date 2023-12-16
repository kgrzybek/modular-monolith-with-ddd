using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals;
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
        [ProducesResponseType(typeof(List<MeetingGroupProposalDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemberMeetingGroupProposals()
        {
            var meetingGroupProposals = await _meetingsModule.ExecuteQueryAsync(
                new GetMemberMeetingGroupProposalsQuery());

            return Ok(meetingGroupProposals);
        }

        [HttpGet("all")]
        [HasPermission(MeetingsPermissions.GetMeetingGroupProposals)]
        [ProducesResponseType(typeof(List<MeetingGroupProposalDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMeetingGroupProposals(int? page, int? perPage)
        {
            var meetingGroupProposals = await _meetingsModule.ExecuteQueryAsync(
                new GetAllMeetingGroupProposalsQuery(page, perPage));

            return Ok(meetingGroupProposals);
        }

        [HttpPost("")]
        [HasPermission(MeetingsPermissions.ProposeMeetingGroup)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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