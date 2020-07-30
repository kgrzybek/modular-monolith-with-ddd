using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class AcceptMeetingGroupProposalCommand : CommandBase<Unit>
    {
        public AcceptMeetingGroupProposalCommand(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal Guid MeetingGroupProposalId { get; }
    }
}