using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    public class UnlikeMeetingCommentCommand : CommandBase<Unit>
    {
        public Guid MeetingCommentId { get; }

        public UnlikeMeetingCommentCommand(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}