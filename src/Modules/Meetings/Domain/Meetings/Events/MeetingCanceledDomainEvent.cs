using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class MeetingCanceledDomainEvent : DomainEventBase
    {
        public MeetingCanceledDomainEvent(MeetingId meetingId, MemberId cancelMemberId, DateTime cancelDate)
        {
            MeetingId = meetingId;
            CancelMemberId = cancelMemberId;
            CancelDate = cancelDate;
        }

        public MeetingId MeetingId { get; }

        public MemberId CancelMemberId { get; }

        public DateTime CancelDate { get; }
    }
}