using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike
{
    public class RemoveMeetingCommentLikeCommand : CommandBase<Unit>
    {
        public Guid MeetingCommentId { get; }

        public RemoveMeetingCommentLikeCommand(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}