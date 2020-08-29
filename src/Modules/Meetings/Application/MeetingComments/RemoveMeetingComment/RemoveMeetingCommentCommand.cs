using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment
{
    public class RemoveMeetingCommentCommand : CommandBase<Unit>
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