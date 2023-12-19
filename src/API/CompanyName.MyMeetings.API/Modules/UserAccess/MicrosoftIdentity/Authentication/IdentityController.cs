using AutoMapper;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserAccount;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserPermissions;
using Microsoft.AspNetCore.Mvc;
using IdentityContracts = CompanyName.MyMeetings.Contracts.V1.Users.Identity;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authentication;

[Route("api/userAccess/identity")]
[ApiExplorerSettings(GroupName = "Authenticated User")]
public class IdentityController : ApplicationController
{
    private readonly IMapper _mapper;
    private readonly IUserAccessModule _userAccessModule;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public IdentityController(IUserAccessModule userAccessModule, IExecutionContextAccessor executionContextAccessor, IMapper mapper)
    {
        _mapper = mapper;
        _userAccessModule = userAccessModule;
        _executionContextAccessor = executionContextAccessor;
    }

    [HttpGet("user-account")]
    [HasPermission(UsersPermissions.GetUserAccounts)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserAccount()
    {
        var result = await _userAccessModule.ExecuteQueryAsync(new GetUserAccountQuery(_executionContextAccessor.UserId));
        if (result.HasError)
        {
            return FromResponse(result);
        }

        var userAccount = _mapper.Map<IdentityContracts.UserAccountDto>(result.Value);
        return result.ToApiResult(userAccount);
    }

    [HttpGet("permissions")]
    [HasPermission(UsersPermissions.GetUserAccounts)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAuthenticatedUserPermissions()
    {
        var result = await _userAccessModule.ExecuteQueryAsync(new GetUserPermissionsQuery(_executionContextAccessor.UserId));
        if (result.HasError)
        {
            return FromResponse(result);
        }

        var permissions = _mapper.Map<IEnumerable<IdentityContracts.UserPermissionDto>>(result.Value);
        return result.ToApiResult(permissions);
    }
}