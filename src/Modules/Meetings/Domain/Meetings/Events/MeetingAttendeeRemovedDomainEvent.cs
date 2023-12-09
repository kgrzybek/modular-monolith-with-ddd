using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingAttendeeRemovedDomainEvent : DomainEventBase
    {
        public MeetingAttendeeRemovedDomainEvent(MemberId memberId, MeetingId meetingId, string reason)
        {
            MemberId = memberId;
            MeetingId = meetingId;
            Reason = reason;
        }

        public MemberId MemberId { get; }

        public MeetingId MeetingId { get; }

        public string Reason { get; }
    }
}