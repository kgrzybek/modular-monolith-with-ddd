using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.CreateRole;

internal class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Result<Guid>>
{
    private readonly RoleManager<Role> _roleManager;

    public CreateRoleCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new Role(request.Name));
        if (!result.Succeeded)
        {
            return result.Errors.Map().Combine();
        }

        var role = await _roleManager.FindByNameAsync(request.Name);

        var permissions = request.Permissions ?? Enumerable.Empty<string>();
        foreach (var permission in permissions)
        {
            await _roleManager.AddClaimAsync(role!, new Claim(CustomClaimTypes.Permission, permission));
            role = await _roleManager.FindByNameAsync(role!.Name!);
        }

        return Result.Ok(role!.Id);
    }
}