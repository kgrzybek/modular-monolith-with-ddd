using System.Data;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members
{
    public class MembersQueryHelper
    {
        public static async Task<MemberDto> GetMember(MemberId memberId, IDbConnection connection)
        {
            return await connection.QuerySingleAsync<MemberDto>(
                "SELECT " +
                                                                "[Member].Id, " +
                                                                "[Member].[Name], " +
                                                                "[Member].[Login], " +
                                                                "[Member].[Email] " +
                                                                "FROM [meetings].[v_Members] AS [Member] " +
                                                                "WHERE [Member].[Id] = @Id", new
                                                                {
                                                                    Id = memberId.Value
                                                                });
        }
    }
}