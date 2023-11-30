using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals
{
    public class GetMemberMeetingGroupProposalsQuery : QueryBase<List<MeetingGroupProposalDto>>
    {
        public GetMemberMeetingGroupProposalsQuery()
        {
        }
    }
}