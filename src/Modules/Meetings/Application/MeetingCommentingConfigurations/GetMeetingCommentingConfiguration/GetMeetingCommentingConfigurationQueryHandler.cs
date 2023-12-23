using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration
{
    internal class GetMeetingCommentingConfigurationQueryHandler : IQueryHandler<GetMeetingCommentingConfigurationQuery, MeetingCommentingConfigurationDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentingConfigurationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingCommentingConfigurationDto> Handle(GetMeetingCommentingConfigurationQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT
                                    [MeetingCommentingConfiguration].[MeetingId] AS [{nameof(MeetingCommentingConfigurationDto.MeetingId)}],
                                    [MeetingCommentingConfiguration].[IsCommentingEnabled] AS [{nameof(MeetingCommentingConfigurationDto.IsCommentingEnabled)}]
                                FROM [meetings].[MeetingCommentingConfigurations] AS [MeetingCommentingConfiguration]
                                WHERE [MeetingCommentingConfiguration].[MeetingId] = @MeetingId
                                """;

            return await connection.QuerySingleOrDefaultAsync<MeetingCommentingConfigurationDto>(sql, new { query.MeetingId });
        }
    }
}