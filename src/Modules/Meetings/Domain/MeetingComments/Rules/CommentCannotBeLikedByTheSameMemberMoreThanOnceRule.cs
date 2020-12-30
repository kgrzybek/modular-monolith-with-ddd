using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCannotBeLikedByTheSameMemberMoreThanOnceRule : IBusinessRule
    {
        private readonly int _memberCommentLikesCount;

        public CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(int memberCommentLikesCount)
        {
            _memberCommentLikesCount = memberCommentLikesCount;
        }

        public bool IsBroken() => _memberCommentLikesCount > 0;

        public string Message => "Member cannot like one comment more than once.";
    }
}