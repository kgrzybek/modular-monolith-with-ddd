using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments
{
    public class GetMeetingCommentsQuery : QueryBase<List<MeetingCommentDto>>
    {
        public Guid MeetingId { get; }

        public GetMeetingCommentsQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}