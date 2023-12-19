using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRolePermissions;

internal class GetRolePermissionsQueryHandler : IQueryHandler<GetRolePermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly RoleManager<Role> _roleManager;

    public GetRolePermissionsQueryHandler(
        RoleManager<Role> roleManager,
        ISqlConnectionFactory connectionFactory)
    {
        _roleManager = roleManager;
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var role = _roleManager.Roles
            .Where(x => x.Id == request.RoleId)
            .Select(x => x)
            .FirstOrDefault();

        if (role is null)
        {
            return Errors.General.NotFound(request.RoleId, "Role");
        }

        var roleClaims = await _roleManager.GetClaimsAsync(role);
        var claims = roleClaims
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
}