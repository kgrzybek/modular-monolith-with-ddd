using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes
{
    public interface IMeetingMemberCommentLikesRepository
    {
        Task<int> CountMemberCommentLikesAsync(MemberId memberId, MeetingCommentId commentId);
    }
}