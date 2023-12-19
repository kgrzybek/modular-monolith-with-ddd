using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.RenameRole;

internal class RenameRoleCommandHander : ICommandHandler<RenameRoleCommand, Result>
{
    private readonly RoleManager<Role> _roleManager;

    public RenameRoleCommandHander(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> Handle(RenameRoleCommand request, CancellationToken cancellationToken)
    {
        var role = (from r in _roleManager.Roles
                    where r.Id == request.RoleId
                    select r).SingleOrDefault();
        if (role == null)
        {
            return Errors.General.NotFound(request.RoleId, "User role");
        }

        var result = await _roleManager.SetRoleNameAsync(role, request.Name);
        if (!result.Succeeded)
        {
            return result.Errors.Map().Combine();
        }

        return Result.Ok();
    }
}