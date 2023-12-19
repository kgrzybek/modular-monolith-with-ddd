using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetUserRoles;

public class GetUserRolesQuery : QueryBase<Result<IEnumerable<RoleDto>>>
{
    public GetUserRolesQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}