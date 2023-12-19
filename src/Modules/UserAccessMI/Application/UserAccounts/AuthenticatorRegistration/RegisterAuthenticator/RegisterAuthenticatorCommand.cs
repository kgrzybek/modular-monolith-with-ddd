using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.RegisterAuthenticator;

public class RegisterAuthenticatorCommand : CommandBase<Result>
{
    public RegisterAuthenticatorCommand(Guid userId, string otpCode)
    {
        UserId = userId;
        OtpCode = otpCode;
    }

    public Guid UserId { get; }

    public string OtpCode { get; }
}