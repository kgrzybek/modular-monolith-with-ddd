using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalVerificationRequestedDomainEvent : DomainEventBase
    {
        internal MeetingGroupProposalVerificationRequestedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        public MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}