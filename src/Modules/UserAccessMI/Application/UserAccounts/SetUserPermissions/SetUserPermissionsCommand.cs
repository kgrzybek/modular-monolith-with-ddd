using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserPermissions;

public class SetUserPermissionsCommand : CommandBase<Result>
{
    public SetUserPermissionsCommand(Guid userId, IEnumerable<string> permissions)
    {
        UserId = userId;
        Permissions = permissions;
    }

    public Guid UserId { get; }

    public IEnumerable<string> Permissions { get; }
}