using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class CommentReplyCreatedDomainEvent : DomainEventBase
    {
        public Guid ReplyId { get; }

        public Guid InReplyToCommentId { get; }

        public string Reply { get; }

        public CommentReplyCreatedDomainEvent(Guid replyId, Guid inReplyToCommentId, string reply)
        {
            ReplyId = replyId;
            InReplyToCommentId = inReplyToCommentId;
            Reply = reply;
        }
    }
}