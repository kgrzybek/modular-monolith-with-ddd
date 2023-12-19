using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.GetUserRegistration
{
    internal class GetUserRegistrationQueryHandler : IQueryHandler<GetUserRegistrationQuery, Result<UserRegistrationDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<UserRegistrationDto>> Handle(GetUserRegistrationQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[UserRegistration].[Id], " +
                               "[UserRegistration][UserName] AS [Login], " +
                               "[UserRegistration].[Email], " +
                               "[UserRegistration].[FirstName], " +
                               "[UserRegistration].[LastName], " +
                               "[UserRegistration].[Name], " +
                               "[UserRegistration].[StatusCode] " +
                               "FROM [usersmi].[v_UserRegistrations] AS [UserRegistration] " +
                               "WHERE [UserRegistration].[Id] = @UserRegistrationId";

            var userRegistration = await connection.QuerySingleAsync<UserRegistrationDto>(
                sql,
                new
                {
                    query.UserRegistrationId
                });

            if (userRegistration is null)
            {
                return Errors.General.NotFound(query.UserRegistrationId, "User registration");
            }

            return userRegistration;
        }
    }
}