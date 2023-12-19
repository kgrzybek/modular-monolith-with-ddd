using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserPermissions;

public class GetUserPermissionsQuery : QueryBase<Result<IEnumerable<UserPermissionDto>>>
{
    public GetUserPermissionsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}