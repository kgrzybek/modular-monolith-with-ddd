using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees
{
    internal class GetMeetingAttendeesQueryHandler : IQueryHandler<GetMeetingAttendeesQuery, List<MeetingAttendeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingAttendeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingAttendeeDto>> Handle(GetMeetingAttendeesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = $"""
                                SELECT
                                    [MeetingAttendee].[FirstName] AS [{nameof(MeetingAttendeeDto.FirstName)}],
                                    [MeetingAttendee].[LastName] AS [{nameof(MeetingAttendeeDto.LastName)}],
                                    [MeetingAttendee].[RoleCode] AS [{nameof(MeetingAttendeeDto.RoleCode)}],
                                    [MeetingAttendee].[DecisionDate] AS [{nameof(MeetingAttendeeDto.DecisionDate)}],
                                    [MeetingAttendee].[GuestsNumber] AS [{nameof(MeetingAttendeeDto.GuestsNumber)}],
                                    [MeetingAttendee].[AttendeeId] AS [{nameof(MeetingAttendeeDto.AttendeeId)}]
                                FROM [meetings].[v_MeetingAttendees] AS [MeetingAttendee]
                                WHERE [MeetingAttendee].[MeetingId] = @MeetingId
                                """;
            return (await connection.QueryAsync<MeetingAttendeeDto>(
                sql,
                new
                {
                    query.MeetingId
                })).AsList();
        }
    }
}