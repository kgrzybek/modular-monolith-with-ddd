using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingCreatedDomainEvent : DomainEventBase
    {
        public MeetingCreatedDomainEvent(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }

        public MeetingId MeetingId { get; }
    }
}