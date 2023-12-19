using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ChangeEmailAddress;

public class ChangeEmailAddressCommand : CommandBase<Result<string>>
{
    public ChangeEmailAddressCommand(long userId, string newEmailAddress, string token)
    {
        UserId = userId;
        NewEmailAddress = newEmailAddress;
        Token = token;
    }

    public long UserId { get; }

    public string NewEmailAddress { get; }

    public string Token { get; }
}