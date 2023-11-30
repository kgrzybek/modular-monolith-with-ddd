using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal
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