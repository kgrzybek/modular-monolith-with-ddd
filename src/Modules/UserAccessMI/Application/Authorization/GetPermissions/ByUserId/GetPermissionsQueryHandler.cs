using System.Security.Claims;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions.ByUserId;

internal class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly RoleManager<Role> _roleManager;

    public GetPermissionsQueryHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<Role> roleManager,
        ISqlConnectionFactory connectionFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var roleNames = await _userManager.GetRolesAsync(user);
        var roleClaims = await GetClaimsAsync(roleNames);
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claims = roleClaims.Union(userClaims)
            .Where(x => x.Type == CustomClaimTypes.Permission)
            .Select(x => x.Value)
            .ToList();

        // Short circuit
        if (!claims.Any())
        {
            return Result.Ok(Enumerable.Empty<PermissionDto>());
        }

        using (var connection = _connectionFactory.CreateNewConnection())
        {
            string query = $@"
                    SELECT [P].[Code]           AS {nameof(PermissionDto.Code)},
                           [P].[Name]           AS {nameof(PermissionDto.Name)},
                           [P].[Description]    AS {nameof(PermissionDto.Description)}
                      FROM [usersmi].[Permissions] AS [P]
                     WHERE [P].[Code] IN @Claims";

            var permissions = await connection.QueryAsync<PermissionDto>(
                new CommandDefinition(query, new { Claims = claims }, cancellationToken: cancellationToken));

            return Result.Ok(permissions);
        }
    }

    private async Task<List<Claim>> GetClaimsAsync(IEnumerable<string> roleNames)
    {
        var claims = new List<Claim>();
        foreach (var roleName in roleNames)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
            {
                continue;
            }

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            claims.AddRange(roleClaims ?? Enumerable.Empty<Claim>());
        }

        return claims;
    }
}