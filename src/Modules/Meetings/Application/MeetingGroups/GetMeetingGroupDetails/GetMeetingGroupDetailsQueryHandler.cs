using System.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails
{
    internal class GetMeetingGroupDetailsQueryHandler : IQueryHandler<GetMeetingGroupDetailsQuery, MeetingGroupDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingGroupDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingGroupDetailsDto> Handle(GetMeetingGroupDetailsQuery query, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT
                                    [MeetingGroup].[Id] AS [{nameof(MeetingGroupDetailsDto.Id)}],
                                    [MeetingGroup].[Name] AS [{nameof(MeetingGroupDetailsDto.Name)}],
                                    [MeetingGroup].[Description] AS [{nameof(MeetingGroupDetailsDto.Description)}],
                                    [MeetingGroup].[LocationCity] AS [{nameof(MeetingGroupDetailsDto.LocationCity)}],
                                    [MeetingGroup].[LocationCountryCode] AS [{nameof(MeetingGroupDetailsDto.LocationCountryCode)}]
                                FROM [meetings].[v_MeetingGroups] AS [MeetingGroup]
                                WHERE [MeetingGroup].[Id] = @MeetingGroupId
                                """;
            var meetingGroup = await connection.QuerySingleAsync<MeetingGroupDetailsDto>(
                sql,
                new
                {
                    query.MeetingGroupId
                });

            meetingGroup.MembersCount = await GetMembersCount(query.MeetingGroupId, connection);

            return meetingGroup;
        }

        private static async Task<int> GetMembersCount(Guid meetingGroupId, IDbConnection connection)
        {
            const string sql = """
                               SELECT COUNT(*)
                               FROM [meetings].[v_MemberMeetingGroups] AS [MemberMeetingGroup]
                               WHERE
                                   [MemberMeetingGroup].[Id] = @MeetingGroupId AND
                                   [MemberMeetingGroup].[IsActive] = 1
                               """;

            return await connection.ExecuteScalarAsync<int>(
                sql,
                new
                {
                    meetingGroupId
                });
        }
    }
}