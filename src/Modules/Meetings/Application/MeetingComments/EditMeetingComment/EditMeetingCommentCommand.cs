using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment
{
    public class EditMeetingCommentCommand : CommandBase
    {
        public Guid MeetingCommentId { get; }

        public string EditedComment { get; }

        public EditMeetingCommentCommand(Guid meetingCommentId, string editedComment)
        {
            EditedComment = editedComment;
            MeetingCommentId = meetingCommentId;
        }
    }
}