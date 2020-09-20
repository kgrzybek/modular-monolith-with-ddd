using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events
{
    public class MeetingCommentingDisabledDomainEvent : DomainEventBase
    {
        public MeetingId MeetingId { get; }

        public MeetingCommentingDisabledDomainEvent(MeetingId meetingId)
        {
            MeetingId = meetingId;
        }
    }
}