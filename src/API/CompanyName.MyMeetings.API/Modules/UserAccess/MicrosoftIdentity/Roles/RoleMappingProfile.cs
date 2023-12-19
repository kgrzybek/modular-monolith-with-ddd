using AutoMapper;
using CompanyName.MyMeetings.Contracts.V1.Users.Roles;
using Application = CompanyName.MyMeetings.Modules.UserAccessMI.Application;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Roles;

internal class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<Application.Roles.GetRoles.RoleDto, RoleDto>();
        CreateMap<Application.Roles.GetRolePermissions.PermissionDto, PermissionDto>();
    }
}