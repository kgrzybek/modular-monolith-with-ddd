using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events
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