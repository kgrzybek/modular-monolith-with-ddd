using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.IdentityServer
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims.Where(x => x.Type == CustomClaimTypes.Roles).ToList());
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == CustomClaimTypes.Name));
            context.IssuedClaims.Add(context.Subject.Claims.Single(x => x.Type == CustomClaimTypes.Email));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(context.IsActive);
        }
    }
}