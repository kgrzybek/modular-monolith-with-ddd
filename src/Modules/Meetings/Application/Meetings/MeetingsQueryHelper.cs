using System.Data;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MeetingsQueryHelper
    {
        public static async Task<MeetingDto> GetMeeting(MeetingId meetingId, IDbConnection connection)
        {
            const string sql = $"""
                       SELECT
                           [Meeting].Id as [{nameof(MeetingDto.Id)}],
                           [Meeting].Title as [{nameof(MeetingDto.Title)}],,
                           [Meeting].Description as [{nameof(MeetingDto.Description)}],,
                           [Meeting].LocationAddress as [{nameof(MeetingDto.LocationAddress)}],
                           [Meeting].LocationCity as [{nameof(MeetingDto.LocationCity)}],,
                           [Meeting].LocationPostalCode as [{nameof(MeetingDto.LocationPostalCode)}],,
                           [Meeting].TermStartDate as [{nameof(MeetingDto.TermStartDate)}],,
                           [Meeting].TermEndDate as [{nameof(MeetingDto.TermEndDate)}],
                       FROM [meetings].[v_Meetings] AS [Meeting]
                       WHERE [Meeting].[Id] = @Id
                       """;
            return await connection.QuerySingleAsync<MeetingDto>(
                sql,
                new
                {
                    Id = meetingId.Value
                });
        }
    }
}