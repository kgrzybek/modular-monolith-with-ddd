using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRoles.ById;

public class GetRolesQuery : QueryBase<Result<RoleDto>>
{
    public GetRolesQuery(Guid roleId)
    {
        RoleId = roleId;
    }

    public Guid RoleId { get; }
}