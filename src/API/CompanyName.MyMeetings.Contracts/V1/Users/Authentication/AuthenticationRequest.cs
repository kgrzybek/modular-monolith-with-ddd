namespace CompanyName.MyMeetings.Contracts.V1.Users.Authentication;

public class AuthenticationRequest
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}