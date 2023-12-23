using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments
{
    internal class GetMeetingCommentsQueryHandler : IQueryHandler<GetMeetingCommentsQuery, List<MeetingCommentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentDto>> Handle(GetMeetingCommentsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT
                                    [MeetingComment].[Id] AS [{nameof(MeetingCommentDto.Id)}],
                                    [MeetingComment].[InReplyToCommentId] AS [{nameof(MeetingCommentDto.InReplyToCommentId)}],
                                    [MeetingComment].[AuthorId] AS [{nameof(MeetingCommentDto.AuthorId)}],
                                    [MeetingComment].[Comment] AS [{nameof(MeetingCommentDto.Comment)}],
                                    [MeetingComment].[CreateDate] AS [{nameof(MeetingCommentDto.CreateDate)}],
                                    [MeetingComment].[EditDate] AS [{nameof(MeetingCommentDto.EditDate)}],
                                    [MeetingComment].[LikesCount] AS [{nameof(MeetingCommentDto.LikesCount)}]
                                FROM [meetings].[v_MeetingComments] AS [MeetingComment]
                                WHERE [MeetingComment].[MeetingId] = @MeetingId
                                """;
            var meetingComments = await connection.QueryAsync<MeetingCommentDto>(sql, new { query.MeetingId });

            return meetingComments.AsList();
        }
    }
}