using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLikeId : TypedIdValueBase
    {
        public MeetingMemberCommentLikeId(Guid value)
            : base(value)
        {
        }
    }
}