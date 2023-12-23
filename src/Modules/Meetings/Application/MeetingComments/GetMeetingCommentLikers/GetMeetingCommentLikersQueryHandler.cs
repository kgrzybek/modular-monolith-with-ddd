using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers
{
    internal class GetMeetingCommentLikersQueryHandler : IQueryHandler<GetMeetingCommentLikersQuery, List<MeetingCommentLikerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentLikersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentLikerDto>> Handle(GetMeetingCommentLikersQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                               SELECT
                                  [Liker].[Id] as [{nameof(MeetingCommentLikerDto.Id)}],
                                  [Liker].[Name] as [{nameof(MeetingCommentLikerDto.Name)}]
                               FROM [meetings].[Members] AS [Liker]
                                   INNER JOIN [meetings].[MeetingMemberCommentLikes] AS [Like]
                                         ON [Liker].[Id] = [Like].[MemberId]
                               WHERE [Like].[MeetingCommentId] = @MeetingCommentId
                               """;

            var meetingCommentLikers = await connection.QueryAsync<MeetingCommentLikerDto>(sql, new { query.MeetingCommentId });

            return meetingCommentLikers.AsList();
        }
    }
}