namespace CompanyName.MyMeetings.Contracts.V1.Users.Authentication;

public class TokenResultDto
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}