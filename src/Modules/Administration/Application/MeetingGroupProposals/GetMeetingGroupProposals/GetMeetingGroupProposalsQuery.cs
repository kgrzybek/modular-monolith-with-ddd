using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals
{
    public class GetMeetingGroupProposalsQuery : QueryBase<List<MeetingGroupProposalDto>>
    {
        public GetMeetingGroupProposalsQuery()
        {
        }
    }
}