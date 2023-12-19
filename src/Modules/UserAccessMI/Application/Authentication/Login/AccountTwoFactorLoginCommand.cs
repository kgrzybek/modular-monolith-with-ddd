using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

public class AccountTwoFactorLoginCommand : CommandBase<AuthenticationResult>
{
    public AccountTwoFactorLoginCommand(Guid userId, string provider, string token)
    {
        UserId = userId;
        Provider = provider;
        Token = token;
    }

    public Guid UserId { get; }

    public string Provider { get; }

    public string Token { get; }
}