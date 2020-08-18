using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingNotAttendeeAddedDomainEvent : DomainEventBase
    {
        public MeetingId MeetingId { get; }

        public MemberId MemberId { get; }

        public MeetingNotAttendeeAddedDomainEvent(MeetingId meetingId, MemberId memberId)
        {
            MeetingId = meetingId;
            MemberId = memberId;
        }
    }
}