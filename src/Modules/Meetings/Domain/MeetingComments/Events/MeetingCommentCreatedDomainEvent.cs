using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events
{
    public class MeetingCommentCreatedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public MeetingCommentCreatedDomainEvent(MeetingCommentId meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}