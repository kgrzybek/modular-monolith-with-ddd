using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike
{
    public class AddMeetingCommentLikeCommand : CommandBase
    {
        public Guid MeetingCommentId { get; }

        public AddMeetingCommentLikeCommand(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}