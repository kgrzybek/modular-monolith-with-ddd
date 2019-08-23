using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposalAcceptedDomainEvent : DomainEventBase
    {
        public MeetingGroupProposalId MeetingGroupProposalId { get; }

        public MeetingGroupProposalAcceptedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}