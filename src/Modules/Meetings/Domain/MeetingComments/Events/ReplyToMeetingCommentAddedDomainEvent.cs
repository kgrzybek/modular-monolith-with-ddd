using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events
{
    public class ReplyToMeetingCommentAddedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public MeetingCommentId InReplyToCommentId { get; }

        public string Reply { get; }

        public ReplyToMeetingCommentAddedDomainEvent(MeetingCommentId meetingCommentId, MeetingCommentId inReplyToCommentId, string reply)
        {
            MeetingCommentId = meetingCommentId;
            InReplyToCommentId = inReplyToCommentId;
            Reply = reply;
        }
    }
}