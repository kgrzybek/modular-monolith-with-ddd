using CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.IdentityServer
{
    internal class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserAccessModule _userAccessModule;

        public ResourceOwnerPasswordValidator(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var authenticationResult = await _userAccessModule.ExecuteCommandAsync(
                new AuthenticateCommand(context.UserName, context.Password));

            if (!authenticationResult.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    authenticationResult.AuthenticationError);

                return;
            }

            context.Result = new GrantValidationResult(
                authenticationResult.User.Id.ToString(),
                "forms",
                authenticationResult.User.Claims);
        }
    }
}