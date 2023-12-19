using AutoMapper;
using CompanyName.MyMeetings.Contracts.V1.Users.Identity;
using Application = CompanyName.MyMeetings.Modules.UserAccessMI.Application;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authentication;

internal class AuthenticationMappingProfile : Profile
{
    public AuthenticationMappingProfile()
    {
        AllowNullCollections = true;

        CreateMap<Application.Identity.GetUserAccount.UserAccountDto, UserAccountDto>();
        CreateMap<Application.Identity.GetUserPermissions.UserPermissionDto, UserPermissionDto>();
    }
}