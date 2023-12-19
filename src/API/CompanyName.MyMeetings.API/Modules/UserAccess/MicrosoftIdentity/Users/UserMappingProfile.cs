using AutoMapper;
using CompanyName.MyMeetings.Contracts.V1.Users.Users;
using Application = CompanyName.MyMeetings.Modules.UserAccessMI.Application;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Users;

internal class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<Application.UserAccounts.GetUserAccounts.UserAccountDto, UserAccountDto>();

        CreateMap<Application.Authorization.GetUserRoles.RoleDto, RoleDto>();
        CreateMap<Application.Authorization.GetPermissions.PermissionDto, PermissionDto>();
    }
}