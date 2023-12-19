using AutoMapper;
using CompanyName.MyMeetings.Contracts.V1.Users.Authorization;
using Application = CompanyName.MyMeetings.Modules.UserAccessMI.Application;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authorization;

public class AuthorizationMappingProfile : Profile
{
    public AuthorizationMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<Application.Authorization.GetPermissions.PermissionDto, PermissionDto>();
    }
}