using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.UnlockUserAccount;

public class UnlockUserAccountCommand : CommandBase<Result>
{
    public UnlockUserAccountCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}