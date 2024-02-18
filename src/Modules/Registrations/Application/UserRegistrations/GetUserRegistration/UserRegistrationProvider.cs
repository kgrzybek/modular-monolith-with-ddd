using System.Data;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.GetUserRegistration;

internal static class UserRegistrationProvider
{
    internal static async Task<UserRegistrationDto> GetById(
        IDbConnection connection,
        Guid userRegistrationId)
    {
        const string sql = $"""
                            SELECT
                                [UserRegistration].[Id] as [{nameof(UserRegistrationDto.Id)}],
                                [UserRegistration].[Login] as [{nameof(UserRegistrationDto.Login)}],
                                [UserRegistration].[Email] as [{nameof(UserRegistrationDto.Email)}],
                                [UserRegistration].[FirstName] as [{nameof(UserRegistrationDto.FirstName)}],
                                [UserRegistration].[LastName] as [{nameof(UserRegistrationDto.LastName)}],
                                [UserRegistration].[Name] as [{nameof(UserRegistrationDto.Name)}],
                                [UserRegistration].[StatusCode] as [{nameof(UserRegistrationDto.StatusCode)}],
                                [UserRegistration].[Password] as [{nameof(UserRegistrationDto.Password)}]
                            FROM [registrations].[v_UserRegistrations] AS [UserRegistration]
                            WHERE [UserRegistration].[Id] = @UserRegistrationId
                            """;

        return await connection.QuerySingleAsync<UserRegistrationDto>(
            sql,
            new
            {
                userRegistrationId
            });
    }
}