using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.SetRolePermissions;

public class SetRolePermissionsCommand : CommandBase<Result>
{
    public SetRolePermissionsCommand(Guid roleId, IEnumerable<string> permissions)
    {
        RoleId = roleId;
        Permissions = permissions;
    }

    public Guid RoleId { get; }

    public IEnumerable<string> Permissions { get; }
}