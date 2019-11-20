using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Modules.Administration.MeetingGroupProposals
{
    [Route("api/administration/[controller]")]
    [ApiController]
    public class MeetingGroupProposalsController : ControllerBase
    {
        private readonly IAdministrationModule _administrationModule;

        public MeetingGroupProposalsController(IAdministrationModule administrationModule)
        {
            _administrationModule = administrationModule;
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