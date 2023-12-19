using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.ByUserId;

public class GetPermissionsQuery : QueryBase<Result<IEnumerable<PermissionDto>>>
{
    public GetPermissionsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}