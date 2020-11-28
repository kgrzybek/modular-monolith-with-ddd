using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikes
{
    public class GetMeetingCommentLikersQuery : IQuery<List<MeetingCommentLikerDto>>
    {
        public Guid MeetingCommentId { get; }

        public GetMeetingCommentLikersQuery(Guid meetingCommentId)
        {
            MeetingCommentId = meetingCommentId;
        }
    }
}