using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.RenameRole;

public class RenameRoleCommand : CommandBase<Result>
{
    public RenameRoleCommand(Guid roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }

    public Guid RoleId { get; }

    public string Name { get; }
}