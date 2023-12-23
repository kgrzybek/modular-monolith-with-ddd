using System.Security.Claims;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                   [User].[Id] as [{nameof(UserDto.Id)}],
                                   [User].[Login] as [{nameof(UserDto.Login)}],
                                   [User].[Name] as [{nameof(UserDto.Name)}],
                                   [User].[Email] as [{nameof(UserDto.Email)}],
                                   [User].[IsActive] as [{nameof(UserDto.IsActive)}],
                                   [User].[Password]  as [{nameof(UserDto.Password)}]
                               FROM [users].[v_Users] AS [User] 
                               WHERE [User].[Login] = @Login
                               """;

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(
                sql,
                new
                {
                    request.Login,
                });

            if (user == null)
            {
                return new AuthenticationResult("Incorrect login or password");
            }

            if (!user.IsActive)
            {
                return new AuthenticationResult("User is not active");
            }

            if (!PasswordManager.VerifyHashedPassword(user.Password, request.Password))
            {
                return new AuthenticationResult("Incorrect login or password");
            }

            user.Claims =
            [
                new Claim(CustomClaimTypes.Name, user.Name),
                new Claim(CustomClaimTypes.Email, user.Email)
            ];

            return new AuthenticationResult(user);
        }
    }
}