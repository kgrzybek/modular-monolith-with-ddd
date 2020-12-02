using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLikeRepository : IMeetingMemberCommentLikesRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public Task<int> CountMemberCommentLikesAsync(MemberId memberId, MeetingCommentId meetingCommentId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "COUNT(*) " +
                               "FROM [meetings].[MeetingMemberCommentLikes] AS [Likes]" +
                               "WHERE [Likes].[MemberId] = @MemberId AND [Likes].[MeetingCommentId] = @MeetingCommentId";

            return connection.QuerySingleAsync<int>(
                sql,
                new
                {
                    memberId,
                    meetingCommentId
                });
        }
    }
}