using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment
{
    public class RemoveMeetingCommentCommand : CommandBase<Unit>
    {
        public MeetingCommentId MeetingCommentId { get; }

        public string Reason { get; }

        public RemoveMeetingCommentCommand(MeetingCommentId meetingCommentId, string reason)
        {
            MeetingCommentId = meetingCommentId;
            Reason = reason;
        }
    }
}