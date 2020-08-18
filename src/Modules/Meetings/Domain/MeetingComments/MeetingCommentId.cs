using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Comments
{
    public class MeetingCommentId : TypedIdValueBase
    {
        public MeetingCommentId(Guid value)
            : base(value)
        {
        }
    }
}