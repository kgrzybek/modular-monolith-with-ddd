using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations;

public class UsersCounter : IUsersCounter
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UsersCounter(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public int CountUsersWithLogin(string login)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           "COUNT(*) " +
                           "FROM [usersmi].[v_Users] AS [User]" +
                           "WHERE [User].[Login] = @Login";
        return connection.QuerySingle<int>(
            sql,
            new
            {
                login
            });
    }
}