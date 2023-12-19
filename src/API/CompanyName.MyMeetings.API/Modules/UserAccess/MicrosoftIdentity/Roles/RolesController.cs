using AutoMapper;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.CreateRole;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.DeleteRole;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRolePermissions;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.RenameRole;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.SetRolePermissions;
using Microsoft.AspNetCore.Mvc;
using RolesApplication = CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRoles;
using UserRoleContracts = CompanyName.MyMeetings.Contracts.V1.Users.Roles;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Roles;

[Route("api/userAccess/roles")]
[ApiExplorerSettings(GroupName = "User Access")]
public class RolesController : ApplicationController
{
    private readonly IMapper _mapper;
    private readonly IUserAccessModule _userAccessModule;

    public RolesController(IUserAccessModule userAccessModule, IMapper mapper)
    {
        _mapper = mapper;
        _userAccessModule = userAccessModule;
    }

    [HttpGet]
    [HasPermission(UsersPermissions.GetRoles)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetRoleDirectory()
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new RolesApplication.Directory.GetRolesQuery());
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var userRoles = _mapper.Map<IEnumerable<UserRoleContracts.RoleDto>>(response.Value);
        return response.ToApiResult(userRoles);
    }

    [HttpGet("{roleId}")]
    [HasPermission(UsersPermissions.GetRoles)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetRole(Guid roleId)
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new RolesApplication.ById.GetRolesQuery(roleId));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var userRoles = _mapper.Map<UserRoleContracts.RoleDto>(response.Value);
        return response.ToApiResult(userRoles);
    }

    [HttpPost]
    [HasPermission(UsersPermissions.AddRole)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddRole([FromBody] UserRoleContracts.AddRoleRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new CreateRoleCommand(request.Name, request.Permissions));
        return response.ToApiResult();
    }

    [HttpPatch("{roleId}/rename")]
    [HasPermission(UsersPermissions.RenameRole)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RenameRole(Guid roleId, [FromBody] UserRoleContracts.RenameRoleRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new RenameRoleCommand(roleId, request.Name));
        return response.ToApiResult();
    }

    [HttpDelete("{roleId}")]
    [HasPermission(UsersPermissions.DeleteRole)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new DeleteRoleCommand(roleId));
        return response.ToApiResult();
    }

    [HttpGet("{roleId}/permissions")]
    [HasPermission(UsersPermissions.GetRolePermissions)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetRolePermissions(Guid roleId)
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new GetRolePermissionsQuery(roleId));
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var permissions = _mapper.Map<IEnumerable<UserRoleContracts.PermissionDto>>(response.Value);
        return response.ToApiResult(permissions);
    }

    [HttpPatch("{roleId}/permissions")]
    [HasPermission(UsersPermissions.SetRolePermissions)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetRolePermissions(Guid roleId, [FromBody] UserRoleContracts.SetRolePermissionsRequest request)
    {
        var response = await _userAccessModule.ExecuteCommandAsync(new SetRolePermissionsCommand(roleId, request.Permissions));
        return response.ToApiResult();
    }
}