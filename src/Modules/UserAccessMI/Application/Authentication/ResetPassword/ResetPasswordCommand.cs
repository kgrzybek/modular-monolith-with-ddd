using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.ResetPassword;

public class ResetPasswordCommand : CommandBase<Result>
{
    public ResetPasswordCommand(string token, string emailAddress, string password)
    {
        Token = token;
        EmailAddress = emailAddress;
        Password = password;
    }

    public string Token { get; }

    public string EmailAddress { get; }

    public string Password { get; }
}