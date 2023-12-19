using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetAuthenticatedUser
{
    internal class GetAuthenticatedUserQueryHandler : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetAuthenticatedUserQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                    [User].[Id] as [{nameof(UserDto.Id)}], 
                                    [User].[IsActive] as [{nameof(UserDto.IsActive)}],
                                    [User].[Login] as [{nameof(UserDto.Login)}],
                                    [User].[Email] as [{nameof(UserDto.Email)}],
                                    [User].[Name] as [{nameof(UserDto.Name)}]
                                FROM [users].[v_Users] AS [User]
                                WHERE [User].[Id] = @UserId
                                """;

            return await connection.QuerySingleAsync<UserDto>(sql, new
            {
                _executionContextAccessor.UserId
            });
        }
    }
}