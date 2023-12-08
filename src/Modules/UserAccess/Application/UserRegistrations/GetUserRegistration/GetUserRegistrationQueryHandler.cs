using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.GetUserRegistration
{
    internal class GetUserRegistrationQueryHandler : IQueryHandler<GetUserRegistrationQuery, UserRegistrationDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserRegistrationDto> Handle(GetUserRegistrationQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[UserRegistration].[Id], " +
                               "[UserRegistration].[Login], " +
                               "[UserRegistration].[Email], " +
                               "[UserRegistration].[FirstName], " +
                               "[UserRegistration].[LastName], " +
                               "[UserRegistration].[Name], " +
                               "[UserRegistration].[StatusCode] " +
                               "FROM [users].[v_UserRegistrations] AS [UserRegistration] " +
                               "WHERE [UserRegistration].[Id] = @UserRegistrationId";

            return await connection.QuerySingleAsync<UserRegistrationDto>(
                sql,
                new
                {
                    query.UserRegistrationId
                });
        }
    }
}