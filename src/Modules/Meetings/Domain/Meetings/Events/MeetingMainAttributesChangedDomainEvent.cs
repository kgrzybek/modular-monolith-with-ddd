using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingMainAttributesChangedDomainEvent : DomainEventBase
    {
        public MeetingMainAttributesChangedDomainEvent(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }

        public MeetingId MeetingId { get; }
    }
}