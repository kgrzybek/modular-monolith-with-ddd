using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentCreatedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public MeetingCommentId? InReplyToCommentId { get; }

        public string Comment { get; }

        public MeetingCommentCreatedDomainEvent(MeetingCommentId meetingCommentId, MeetingCommentId? inReplyToCommentId, string comment)
        {
            MeetingCommentId = meetingCommentId;
            InReplyToCommentId = inReplyToCommentId;
            Comment = comment;
        }
    }
}