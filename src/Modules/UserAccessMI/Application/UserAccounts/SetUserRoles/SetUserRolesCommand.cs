using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserRoles;

public class SetUserRolesCommand : CommandBase<Result>
{
    public SetUserRolesCommand(Guid userId, Guid[] roleIds)
    {
        UserId = userId;
        RoleIds = roleIds;
    }

    public Guid UserId { get; }

    public Guid[] RoleIds { get; }
}