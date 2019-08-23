using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class AcceptMeetingGroupProposalCommand : CommandBase
    {
        internal Guid MeetingGroupProposalId { get; }

        public AcceptMeetingGroupProposalCommand(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}