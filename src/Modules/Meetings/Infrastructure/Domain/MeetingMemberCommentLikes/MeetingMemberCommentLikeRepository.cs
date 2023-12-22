using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes
{
    public class MeetingMemberCommentLikeRepository : IMeetingMemberCommentLikesRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly MeetingsContext _meetingsContext;

        public MeetingMemberCommentLikeRepository(ISqlConnectionFactory sqlConnectionFactory, MeetingsContext meetingsContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _meetingsContext = meetingsContext;
        }

        public async Task AddAsync(MeetingMemberCommentLike meetingMemberCommentLike)
        {
            await _meetingsContext.MeetingMemberCommentLikes.AddAsync(meetingMemberCommentLike);
        }

        public async Task<MeetingMemberCommentLike> GetAsync(MemberId memberId, MeetingCommentId meetingCommentId)
        {
            return await _meetingsContext.MeetingMemberCommentLikes.SingleOrDefaultAsync(l =>
                EF.Property<MemberId>(l, "_memberId") == memberId
                && EF.Property<MeetingCommentId>(l, "_meetingCommentId") == meetingCommentId);
        }

        public Task<int> CountMemberCommentLikesAsync(MemberId memberId, MeetingCommentId meetingCommentId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = """
                               SELECT COUNT(*) 
                               FROM [meetings].[MeetingMemberCommentLikes] AS [Likes]
                               WHERE [Likes].[MemberId] = @MemberId AND [Likes].[MeetingCommentId] = @MeetingCommentId
                               """;

            return connection.QuerySingleAsync<int>(
                sql,
                new
                {
                    memberId = memberId.Value,
                    meetingCommentId = meetingCommentId.Value
                });
        }

        public void Remove(MeetingMemberCommentLike meetingMemberCommentLike)
        {
            _meetingsContext.MeetingMemberCommentLikes.Remove(meetingMemberCommentLike);
        }
    }
}