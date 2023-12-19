using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Services.IdentityTokenService;

internal class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly IUserAccessConfiguration _userAccessConfiguration;
    private readonly IUserRefreshTokenRepository _refreshTokenRepository;

    public IdentityTokenClaimService(
        IUserAccessConfiguration userAccessConfiguration,
        IUserRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userAccessConfiguration = userAccessConfiguration;
    }

    public Tokens GenerateTokens(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = GetUserClaims(user);
        var tokenId = Guid.NewGuid().ToString();

        // Add an unique identifier which is used by the refresh token
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, tokenId));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _userAccessConfiguration.GetValidIssuer(),
            Audience = _userAccessConfiguration.GetValidAudience(),

            // JWT tokens are not supposed to be long-lived, quite the opposite.
            // They're designed to be short-lived (5 to 10 minutes) with refresh capabilities.
            Expires = DateTime.UtcNow.AddMinutes(7),
            SigningCredentials = new SigningCredentials(_userAccessConfiguration.GetIssuerSigningKey(), SecurityAlgorithms.HmacSha256)
        };

        // Generate the security object token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Convert the security object token into a string
        var accessToken = tokenHandler.WriteToken(token);
        var refreshToken = RandomString(35) + Guid.NewGuid();
        var userRefreshToken = UserRefreshToken.Create(user, tokenId, refreshToken);

        _refreshTokenRepository.Add(userRefreshToken);
        return new Tokens(accessToken, refreshToken);
    }

    public async Task<Result<Tokens, Error>> GenerateNewTokensAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        var accessTokenValidationResult = await ValidateAccessTokenAsync(accessToken, refreshToken, cancellationToken);
        if (accessTokenValidationResult.IsFailure)
        {
            return accessTokenValidationResult.Error;
        }

        // As refresh tokens should only be used once
        var userRefreshToken = accessTokenValidationResult.Value;

        // .. go ahead an delete the current one
        await _refreshTokenRepository.DeleteAsync(userRefreshToken, cancellationToken);

        // .. and finally generate an new pair of tokens
        return GenerateTokens(userRefreshToken.User);
    }

    public List<Claim> GetUserClaims(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(CustomClaimTypes.Sub, user.Id.ToString()),
            new Claim(CustomClaimTypes.LoginName, user.UserName ?? string.Empty),
            new Claim(CustomClaimTypes.Email, user.Email ?? string.Empty)
        };

        if (!string.IsNullOrEmpty(user.Name))
        {
            claims.Add(new Claim(CustomClaimTypes.Name, user.Name));
        }

        if (!string.IsNullOrEmpty(user.FirstName))
        {
            claims.Add(new Claim(CustomClaimTypes.FirstName, user.FirstName));
        }

        if (!string.IsNullOrEmpty(user.LastName))
        {
            claims.Add(new Claim(CustomClaimTypes.LastName, user.LastName));
        }

        return claims;
    }

    public TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _userAccessConfiguration.GetIssuerSigningKey(),
            ValidIssuer = _userAccessConfiguration.GetValidIssuer(),
            ValidateIssuer = _userAccessConfiguration.ShouldValidateIssuer(),
            ValidAudience = _userAccessConfiguration.GetValidAudience(),
            ValidateAudience = _userAccessConfiguration.ShouldValidateAudience(),
            ValidateLifetime = true,
            RequireExpirationTime = true,

            // Clock skew compensates for server time drift.
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    }

    private async Task<Result<UserRefreshToken, Error>> ValidateAccessTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        // Validate the Jwt format based on the configuration to check if it belongs to the our application.
        var tokenValidationParameters = GetTokenValidationParameters();

        // Here we are saying that we don't care about the accessToken's expiration date
        tokenValidationParameters.ValidateLifetime = false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

        // Check if the token has been encrypted using the algorithm we have specified
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return Errors.Authentication.InvalidToken();
        }

        // Validate access token expiry date
        if (!long.TryParse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value, out long expiryTimeStamp))
        {
            return Errors.Authentication.InvalidToken();
        }

        // Unix time stamp convertion
        var expiryDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryTimeStamp).ToLocalTime();

        if (expiryDate > DateTime.Now)
        {
            return Errors.Authentication.InvalidToken("Token has not yet expired");
        }

        // Validate if the token exists
        UserRefreshToken? userRefreshToken = await _refreshTokenRepository.GetByJwtIdAsync(jwtSecurityToken.Id, cancellationToken);
        if (userRefreshToken is null)
        {
            return Errors.Authentication.InvalidToken("Token not found");
        }

        if (!userRefreshToken.Token.Equals(refreshToken))
        {
            return Errors.Authentication.InvalidToken();
        }

        if (userRefreshToken.IsRevoked)
        {
            // Clean up the revoked token
            await _refreshTokenRepository.DeleteAsync(userRefreshToken, cancellationToken);
            return Errors.Authentication.InvalidToken("Token has been revoked.");
        }

        return userRefreshToken;
    }

    private string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}