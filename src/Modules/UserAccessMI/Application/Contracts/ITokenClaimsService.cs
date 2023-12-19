using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

public interface ITokenClaimsService
{
    TokenValidationParameters GetTokenValidationParameters();

    Tokens GenerateTokens(ApplicationUser user);

    Task<Result<Tokens, Error>> GenerateNewTokensAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);

    List<Claim> GetUserClaims(ApplicationUser user);
}

public record Tokens(string AccessToken, string RefreshToken);