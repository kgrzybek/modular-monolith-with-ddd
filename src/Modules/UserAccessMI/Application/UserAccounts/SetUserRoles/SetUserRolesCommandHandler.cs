using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserRoles;

internal class SetUserRolesCommandHandler : ICommandHandler<SetUserRolesCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SetUserRolesCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result> Handle(SetUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        List<string> roleNames = new();
        foreach (var roleId in request.RoleIds)
        {
            var role = _roleManager.Roles
                .Where(x => x.Id == roleId.ToString())
                .Select(x => new { x.Id, x.Name })
                .FirstOrDefault();

            if (role?.Name is not null)
            {
                roleNames.Add(role.Name);
            }
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = roleNames.ExceptBy(userRoles, y => y).ToList();
        var rolesToRemove = userRoles.ExceptBy(roleNames, y => y).ToList();

        if (rolesToAdd.Any())
        {
            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!result.Succeeded)
            {
                return result.Errors.Map().Combine();
            }
        }

        if (rolesToRemove.Any())
        {
            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!result.Succeeded)
            {
                return result.Errors.Map().Combine();
            }
        }

        return Result.Ok();
    }
}