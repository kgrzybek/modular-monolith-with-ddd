using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MeetingsQueryHelper
    {
        public static async Task<MeetingDto> GetMeeting(MeetingId meetingId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MeetingDto>(
                "SELECT " +
                                                                "[Meeting].Id, " +
                                                                "[Meeting].Title, " +
                                                                "[Meeting].Description, " +
                                                                "[Meeting].LocationAddress, " +
                                                                "[Meeting].LocationCity, " +
                                                                "[Meeting].LocationPostalCode, " +
                                                                "[Meeting].TermStartDate, " +
                                                                "[Meeting].TermEndDate " +
                                                                "FROM [meetings].[v_Meetings] AS [Meeting] " +
                                                                "WHERE [Meeting].[Id] = @Id", 
                new
                                                                {
                                                                    Id = meetingId.Value
                                                                });
        }
    }
}