using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeFeePaidDomainEvent : DomainEventBase
    {
        public MeetingAttendeeFeePaidDomainEvent(MeetingId meetingId, MemberId attendeeId)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
        }

        public MeetingId MeetingId { get; }

        public MemberId AttendeeId { get; }
    }
}