using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups
{
    internal class GetAllMeetingGroupsQueryHandler : IQueryHandler<GetAllMeetingGroupsQuery, List<MeetingGroupDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetAllMeetingGroupsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingGroupDto>> Handle(GetAllMeetingGroupsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                               SELECT 
                                    [MeetingGroup].[Id] as [{nameof(MeetingGroupDto.Id)}] , 
                                    [MeetingGroup].[Name] as [{nameof(MeetingGroupDto.Name)}], 
                                    [MeetingGroup].[Description] as [{nameof(MeetingGroupDto.Description)}], 
                                    [MeetingGroup].[LocationCountryCode] as [{nameof(MeetingGroupDto.LocationCountryCode)}],
                                    [MeetingGroup].[LocationCity] as [{nameof(MeetingGroupDto.LocationCity)}]
                               FROM [meetings].[v_MeetingGroups] AS [MeetingGroup]
                               """;
            var meetingGroups = await connection.QueryAsync<MeetingGroupDto>(sql);

            return meetingGroups.AsList();
        }
    }
}