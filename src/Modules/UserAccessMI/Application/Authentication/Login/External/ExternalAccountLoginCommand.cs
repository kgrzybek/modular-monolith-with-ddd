using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login.External;

public class ExternalAccountLoginCommand : CommandBase<AuthenticationResult>
{
    public ExternalAccountLoginCommand(string provider, string externalUserId, string emailAddress, bool autoCreateUser)
    {
        Provider = provider;
        ExternalUserId = externalUserId;
        EmailAddress = emailAddress;
        AutoCreateUser = autoCreateUser;
    }

    public string Provider { get; }

    public string ExternalUserId { get; }

    public string EmailAddress { get; }

    public bool AutoCreateUser { get; }
}