using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment
{
    public class RemoveMeetingCommentCommand : CommandBase
    {
        public Guid MeetingCommentId { get; }

        public string Reason { get; }

        public RemoveMeetingCommentCommand(Guid meetingCommentId, string reason)
        {
            MeetingCommentId = meetingCommentId;
            Reason = reason;
        }
    }
}