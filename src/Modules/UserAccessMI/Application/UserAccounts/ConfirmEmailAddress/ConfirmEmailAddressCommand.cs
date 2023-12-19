using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ConfirmEmailAddress;

public class ConfirmEmailAddressCommand : CommandBase<Result>
{
    public ConfirmEmailAddressCommand(string emailAddress, string token)
    {
        EmailAddress = emailAddress;
        Token = token;
    }

    public string EmailAddress { get; }

    public string Token { get; }
}