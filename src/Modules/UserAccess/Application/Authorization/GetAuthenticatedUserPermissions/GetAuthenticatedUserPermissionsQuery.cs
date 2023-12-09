using CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authorization.GetAuthenticatedUserPermissions
{
    public class GetAuthenticatedUserPermissionsQuery : QueryBase<List<UserPermissionDto>>
    {
    }
}