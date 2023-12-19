using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Roles.GetRoles.ById;

internal class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, Result<RoleDto>>
{
    private readonly RoleManager<Role> _roleManager;

    public GetRolesQueryHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public Task<Result<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var userRole = (from role in _roleManager.Roles
                        where role.Id == request.RoleId
                        select new RoleDto()
                        {
                            Id = role.Id,
                            Name = role.Name
                        }).SingleOrDefault();

        if (userRole is null)
        {
            return Task.FromResult(Result.Error<RoleDto>(Errors.General.NotFound(request.RoleId, "User role")));
        }

        return Task.FromResult(Result.Ok(userRole));
    }
}