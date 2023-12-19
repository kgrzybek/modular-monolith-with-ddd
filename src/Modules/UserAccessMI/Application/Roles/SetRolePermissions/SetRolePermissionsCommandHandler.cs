using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.SetRolePermissions;

internal class SetRolePermissionsCommandHandler : ICommandHandler<SetRolePermissionsCommand, Result>
{
    private readonly RoleManager<Role> _roleManager;

    public SetRolePermissionsCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> Handle(SetRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        Role? role = GetRoleById(request.RoleId);
        if (role is null)
        {
            return Errors.General.NotFound(request.RoleId, "User role");
        }

        var permissions = request.Permissions ?? Enumerable.Empty<string>();

        var roleClaims = (await _roleManager.GetClaimsAsync(role)) ?? Enumerable.Empty<Claim>();
        var permissionsToAdd = permissions.ExceptBy(roleClaims.Select(x => x.Value), y => y).ToList();
        var claimsToRemove = roleClaims.ExceptBy(permissions, y => y.Value).ToList();

        if (permissionsToAdd.Any())
        {
            foreach (var permission in permissionsToAdd)
            {
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                role = GetRoleById(role.Id)!;
            }
        }

        if (claimsToRemove.Any())
        {
            foreach (var claim in claimsToRemove)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
                role = GetRoleById(role.Id)!;
            }
        }

        return Result.Ok();
    }

    private Role? GetRoleById(Guid roleId)
    {
        return (from r in _roleManager.Roles
                where r.Id == roleId
                select r).SingleOrDefault();
    }
}