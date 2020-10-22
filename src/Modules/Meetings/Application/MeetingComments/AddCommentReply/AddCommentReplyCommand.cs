using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddCommentReply
{
    public class AddCommentReplyCommand : CommandBase<Guid>
    {
        public Guid InReplyToCommentId { get; }

        public string Reply { get; }

        public AddCommentReplyCommand(Guid inReplyToCommentId, string reply)
        {
            InReplyToCommentId = inReplyToCommentId;
            Reply = reply;
        }
    }
}