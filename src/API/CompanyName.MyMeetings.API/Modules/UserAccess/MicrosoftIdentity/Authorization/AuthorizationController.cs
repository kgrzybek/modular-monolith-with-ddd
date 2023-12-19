using AutoMapper;
using CompanyName.MyMeetings.API.Configuration.Authorization;
using CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using AuthorizationApplication = CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions;
using AuthorizationContracts = CompanyName.MyMeetings.Contracts.V1.Users.Authorization;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authorization;

[Route("api/userAccess/authorization")]
[ApiExplorerSettings(GroupName = "User Access")]
public class AuthorizationController : ApplicationController
{
    private readonly IMapper _mapper;
    private readonly IUserAccessModule _userAccessModule;

    public AuthorizationController(IUserAccessModule userAccessModule, IMapper mapper)
    {
        _mapper = mapper;
        _userAccessModule = userAccessModule;
    }

    [HttpGet("permissions")]
    [NoPermissionRequired]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPermissionDirectory()
    {
        var response = await _userAccessModule.ExecuteQueryAsync(new AuthorizationApplication.Directory.GetPermissionsQuery());
        if (response.HasError)
        {
            return FromResponse(response);
        }

        var permissions = _mapper.Map<IEnumerable<AuthorizationContracts.PermissionDto>>(response.Value);
        return response.ToApiResult(permissions);
    }
}