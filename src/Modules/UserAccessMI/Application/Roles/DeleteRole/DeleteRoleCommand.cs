using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.DeleteRole;

public class DeleteRoleCommand : CommandBase<Result>
{
    public DeleteRoleCommand(Guid roleId)
    {
        RoleId = roleId;
    }

    public Guid RoleId { get; }
}