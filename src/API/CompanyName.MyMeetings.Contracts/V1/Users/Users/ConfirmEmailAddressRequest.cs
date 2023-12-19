namespace CompanyName.MyMeetings.Contracts.V1.Users.Users;

public class ConfirmEmailAddressRequest
{
    public string Token { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;
}