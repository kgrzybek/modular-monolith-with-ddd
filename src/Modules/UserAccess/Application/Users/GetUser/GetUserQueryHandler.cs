using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
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
                request.UserId
            });
        }
    }
}