using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    public class GetMeetingGroupProposalQuery : QueryBase<MeetingGroupProposalDto>
    {
        public GetMeetingGroupProposalQuery(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public Guid MeetingGroupProposalId { get; }
    }
}