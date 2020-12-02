using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments
{
    public class MeetingCommentService
    {
        private readonly IMeetingMemberCommentLikesRepository _meetingMemberCommentLikesRepository;

        public MeetingCommentService(IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository)
        {
            _meetingMemberCommentLikesRepository = meetingMemberCommentLikesRepository;
        }

        public async Task<MeetingMemberCommentLike> AddLikeAsync(MeetingGroup meetingGroup, MeetingComment comment, MemberId likerId)
        {
            var meetingMemberCommentLikesCount = await _meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(likerId, comment.Id);

            CheckRule(new CommentCannotBeLikedByTheSameMemberMoreThanOnceRule(meetingMemberCommentLikesCount));

            return comment.Like(meetingGroup, likerId);
        }

        private static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}