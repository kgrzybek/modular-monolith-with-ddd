using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply
{
    public class AddReplyToMeetingCommentCommand : CommandBase<Guid>
    {
        public Guid InReplyToCommentId { get; }

        public string Reply { get; }

        public AddReplyToMeetingCommentCommand(Guid inReplyToCommentId, string reply)
        {
            InReplyToCommentId = inReplyToCommentId;
            Reply = reply;
        }
    }
}