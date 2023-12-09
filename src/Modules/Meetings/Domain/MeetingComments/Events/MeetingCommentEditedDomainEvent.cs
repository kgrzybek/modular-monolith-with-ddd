using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events
{
    public class MeetingCommentEditedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public string EditedComment { get; }

        public MeetingCommentEditedDomainEvent(MeetingCommentId meetingCommentId, string editedComment)
        {
            MeetingCommentId = meetingCommentId;
            EditedComment = editedComment;
        }
    }
}