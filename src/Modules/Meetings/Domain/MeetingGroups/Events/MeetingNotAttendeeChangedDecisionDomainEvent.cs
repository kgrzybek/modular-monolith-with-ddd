using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingNotAttendeeChangedDecisionDomainEvent : DomainEventBase
    {
        public MeetingNotAttendeeChangedDecisionDomainEvent(MemberId memberId, MeetingId meetingId)
        {
            MemberId = memberId;
            MeetingId = meetingId;
        }

        public MemberId MemberId { get; }

        public MeetingId MeetingId { get; }
    }
}