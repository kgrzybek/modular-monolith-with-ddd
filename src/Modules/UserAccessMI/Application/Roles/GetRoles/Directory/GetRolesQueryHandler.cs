using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRoles.Directory;

internal class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, Result<IEnumerable<RoleDto>>>
{
    private readonly RoleManager<Role> _roleManager;

    public GetRolesQueryHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public Task<Result<IEnumerable<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = (from role in _roleManager.Roles
                     select new RoleDto()
                     {
                         Id = role.Id,
                         Name = role.Name
                     }).ToList();

        return Task.FromResult(Result.Ok(roles.AsEnumerable()));
    }
}