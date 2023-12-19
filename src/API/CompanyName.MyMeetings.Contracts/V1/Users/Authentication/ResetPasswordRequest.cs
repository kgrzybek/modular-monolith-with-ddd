using System.ComponentModel.DataAnnotations;

namespace CompanyName.MyMeetings.Contracts.V1.Users.Authentication;

public class ResetPasswordRequest
{
    public string Token { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}