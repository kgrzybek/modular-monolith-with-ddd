using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
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

            const string sql = $"""
                               SELECT 
                                   [UserRegistration].[Id] as [{nameof(UserRegistrationDto.Id)}], 
                                   [UserRegistration].[Login] as [{nameof(UserRegistrationDto.Login)}], 
                                   [UserRegistration].[Email] as [{nameof(UserRegistrationDto.Email)}], 
                                   [UserRegistration].[FirstName] as [{nameof(UserRegistrationDto.FirstName)}],, 
                                   [UserRegistration].[LastName] as [{nameof(UserRegistrationDto.LastName)}], 
                                   [UserRegistration].[Name] as [{nameof(UserRegistrationDto.Name)}],, 
                                   [UserRegistration].[StatusCode] as [{nameof(UserRegistrationDto.StatusCode)}], 
                               FROM [users].[v_UserRegistrations] AS [UserRegistration] 
                               WHERE [UserRegistration].[Id] = @UserRegistrationId
                               """;

            return await connection.QuerySingleAsync<UserRegistrationDto>(
                sql,
                new
                {
                    query.UserRegistrationId
                });
        }
    }
}