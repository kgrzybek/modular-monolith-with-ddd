using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Administration.MeetingGroupProposals
{
    [Route("api/administration/meetingGroupProposals")]
    [ApiController]
    public class MeetingGroupProposalsController : ControllerBase
    {
        private readonly IAdministrationModule _administrationModule;

        public MeetingGroupProposalsController(IAdministrationModule administrationModule)
        {
            _administrationModule = administrationModule;
        }

        [HttpGet("")]
        [HasPermission(AdministrationPermissions.AcceptMeetingGroupProposal)]
        public async Task<IActionResult> GetMeetingGroupProposals()
        {
            var meetingGroupProposals =
                await _administrationModule.ExecuteQueryAsync(new GetMeetingGroupProposalsQuery());

            return Ok(meetingGroupProposals);
        }

        [HttpPatch("{meetingGroupProposalId}/accept")]
        [HasPermission(AdministrationPermissions.AcceptMeetingGroupProposal)]
        public async Task<IActionResult> AcceptMeetingGroupProposal(Guid meetingGroupProposalId)
        {
            await _administrationModule.ExecuteCommandAsync(
                new AcceptMeetingGroupProposalCommand(meetingGroupProposalId));

            return Ok();
        }
    }
}