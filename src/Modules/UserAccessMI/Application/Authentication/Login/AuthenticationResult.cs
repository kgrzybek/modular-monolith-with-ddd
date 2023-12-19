using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

public class AuthenticationResult : Result
{
    public AuthenticationResult()
    {
        RequiresTwoFactor = false;
    }

    public bool IsAuthenticated => ClaimsPrincipal is not null;

    public string? AccessToken { get; private set; }

    public string? RefreshToken { get; private set; }

    public bool RequiresTwoFactor { get; private set; }

    public UserDto? User { get; private set; }

    public ClaimsPrincipal? ClaimsPrincipal { get; private set; }

    public void SetAuthenticatedUser(UserDto user, string accessToken, string refreshToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        User = user;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public void SetAuthenticatedUser(UserDto user, ClaimsPrincipal claimsPrincipal)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));

        User = user;
        ClaimsPrincipal = claimsPrincipal;
    }

    public void SetAuthenticatedUser(UserDto user, string accessToken, string refreshToken, ClaimsPrincipal claimsPrincipal)
    {
        SetAuthenticatedUser(user, accessToken, refreshToken);

        ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));

        ClaimsPrincipal = claimsPrincipal;
    }

    public void StoreTwoFactorAuthentication(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal), "Missing the claims principal.");
        }

        RequiresTwoFactor = true;
        ClaimsPrincipal = claimsPrincipal;
    }
}