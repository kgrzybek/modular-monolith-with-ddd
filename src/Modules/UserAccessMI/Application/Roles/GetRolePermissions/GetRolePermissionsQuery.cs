using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRolePermissions;

public class GetRolePermissionsQuery : QueryBase<Result<IEnumerable<PermissionDto>>>
{
    public GetRolePermissionsQuery(Guid roleId)
    {
        RoleId = roleId;
    }

    public Guid RoleId { get; }
}