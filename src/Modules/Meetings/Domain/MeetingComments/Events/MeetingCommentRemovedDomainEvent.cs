using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentRemovedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public MeetingCommentRemovedDomainEvent(MeetingCommentId meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}