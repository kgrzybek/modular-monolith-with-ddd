using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public class MeetingCommentId : TypedIdValueBase
    {
        public MeetingCommentId(Guid value)
            : base(value)
        {
        }
    }
}