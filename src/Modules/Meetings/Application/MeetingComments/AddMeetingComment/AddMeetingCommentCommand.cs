using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment
{
    public class AddMeetingCommentCommand : CommandBase<Guid>
    {
        public Guid MeetingId { get; }

        public string Comment { get; }

        public AddMeetingCommentCommand(Guid meetingId, string comment)
        {
            MeetingId = meetingId;
            Comment = comment;
        }
    }
}