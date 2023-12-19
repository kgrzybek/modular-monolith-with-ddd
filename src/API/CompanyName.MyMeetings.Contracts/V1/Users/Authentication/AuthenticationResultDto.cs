namespace CompanyName.MyMeetings.Contracts.V1.Users.Authentication;

public class AuthenticationResultDto
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? UserName { get; set; }

    public bool IsLockedOut { get; set; } = false;

    public bool IsNotAllowed { get; set; } = false;

    public bool RequiresTwoFactor { get; set; } = false;
}