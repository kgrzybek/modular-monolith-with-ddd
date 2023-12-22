using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    public class RemoveMeetingCommentLikeCommand : CommandBase
    {
        public Guid MeetingCommentId { get; }

        public RemoveMeetingCommentLikeCommand(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}