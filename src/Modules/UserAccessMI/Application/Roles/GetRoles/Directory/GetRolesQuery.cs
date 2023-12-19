using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRoles.Directory;

public class GetRolesQuery : QueryBase<Result<IEnumerable<RoleDto>>>
{
}