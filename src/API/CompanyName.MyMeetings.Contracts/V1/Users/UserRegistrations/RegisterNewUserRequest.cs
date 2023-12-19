namespace CompanyName.MyMeetings.Contracts.V1.Users.UserRegistrations;

public class RegisterNewUserRequest
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string ConfirmLink { get; set; } = null!;
}