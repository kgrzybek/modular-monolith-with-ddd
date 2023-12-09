using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events
{
    public class MeetingCommentAddedDomainEvent : DomainEventBase
    {
        public MeetingCommentId MeetingCommentId { get; }

        public MeetingId MeetingId { get; }

        public string Comment { get; }

        public MeetingCommentAddedDomainEvent(MeetingCommentId meetingCommentId, MeetingId meetingId, string comment)
        {
            MeetingCommentId = meetingCommentId;
            MeetingId = meetingId;
            Comment = comment;
        }
    }
}