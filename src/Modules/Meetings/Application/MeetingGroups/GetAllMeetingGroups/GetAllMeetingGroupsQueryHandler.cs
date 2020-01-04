using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;
using MediatR;

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

            const string sql = "SELECT " +
                               "[MeetingGroup].[Id], " +
                               "[MeetingGroup].[Name], " +
                               "[MeetingGroup].[Description], " +
                               "[MeetingGroup].[LocationCountryCode], " +
                               "[MeetingGroup].[LocationCity]" +
                               "FROM [meetings].[v_MeetingGroups] AS [MeetingGroup]";
            var meetingGroups = await connection.QueryAsync<MeetingGroupDto>(sql);

            return meetingGroups.AsList();
        }
    }
}