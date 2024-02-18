using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations
{
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

            const string sql = """
                                SELECT COUNT(*) 
                                FROM [registrations].[v_UserRegistrations] AS [UserRegistration]
                                WHERE [UserRegistration].[Login] = @Login
                                """;
            return connection.QuerySingle<int>(
                sql,
                new
                {
                    login
                });
        }
    }
}