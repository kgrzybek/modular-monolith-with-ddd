using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.ByUserId;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CompanyName.MyMeetings.API.Configuration.Authorization;

internal class PermissionAuthorizationHandler : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
{
    private readonly IUserAccessModule _userAccessModule;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public PermissionAuthorizationHandler(
        IUserAccessModule userAccessModule,
        IExecutionContextAccessor executionContextAccessor)
    {
        _userAccessModule = userAccessModule;
        _executionContextAccessor = executionContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement, HasPermissionAttribute attribute)
    {
        if (!_executionContextAccessor.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var userId = _executionContextAccessor.UserId;
        var response = await _userAccessModule.ExecuteQueryAsync(new GetPermissionsQuery(userId));
        if (response.HasError)
        {
            context.Fail();
            return;
        }

        var permissions = response.Value ?? Enumerable.Empty<PermissionDto>();

        // Short circuit if the user owns the administrator privilege.
        if (permissions.Any(x => x.Code.Equals(ApplicationPermissions.Administrator)))
        {
            context.Succeed(requirement);
            return;
        }

        // Check if the user owns the necessary rights.
        if (!IsAuthorized(attribute.Name, permissions))
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }

    private bool IsAuthorized(string permission, IEnumerable<PermissionDto> permissions)
    {
        return permissions.Any(x => x.Code == permission);
    }
}