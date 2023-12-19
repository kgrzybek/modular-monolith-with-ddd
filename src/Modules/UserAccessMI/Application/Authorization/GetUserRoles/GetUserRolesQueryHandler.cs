using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetUserRoles;

internal class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, Result<IEnumerable<RoleDto>>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetUserRolesQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<IEnumerable<RoleDto>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var roleNames = await _userManager.GetRolesAsync(user);
        if (!roleNames.Any())
        {
            return Result.Ok(Enumerable.Empty<RoleDto>());
        }

        var roles = (from role in _roleManager.Roles
                     where roleNames.Contains(role.Name ?? string.Empty)
                     select new RoleDto()
                     {
                         Id = role.Id,
                         Name = role.Name
                     }).ToList();

        return Result.Ok(roles.AsEnumerable());
    }
}