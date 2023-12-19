using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ChangePassword;

public class ChangePasswordCommand : CommandBase<Result>
{
    public ChangePasswordCommand(long userId, string currentPassword, string newPassword)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }

    public long UserId { get; }

    public string CurrentPassword { get; }

    public string NewPassword { get; }
}