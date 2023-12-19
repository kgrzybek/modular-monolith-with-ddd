using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login.External;

internal class ExternalAccountLoginCommandHandler : ICommandHandler<ExternalAccountLoginCommand, AuthenticationResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ExternalAccountLoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenClaimsService tokenClaimsService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<AuthenticationResult> Handle(ExternalAccountLoginCommand request, CancellationToken cancellationToken)
    {
        var response = new AuthenticationResult();

        // Try to find the user by the provider and external user unique identifier
        // This will return the user we have that is link to this external account
        var user = await _userManager.FindByLoginAsync(request.Provider, request.ExternalUserId);

        // If the user is null, so we have never seen this external user before
        if (user is null)
        {
            // ... We have a few options, but in the case we have the user's email address
            var email = request.EmailAddress;
            if (!string.IsNullOrEmpty(email))
            {
                // we check if we can find an user by that email address
                user = await _userManager.FindByEmailAsync(email);

                // If we still have no user we are going to auto create the user if enabled
                if (user == null)
                {
                    if (!request.AutoCreateUser)
                    {
                        response.AddError(Errors.UserAccess.InvalidUserNameOrPassword);
                        return response;
                    }

                    user = new ApplicationUser(email) { Email = email };

                    // Create the user without a password
                    await _userManager.CreateAsync(user);
                }

                // Finally we have to link the user with the external account
                await _userManager.AddLoginAsync(user, new UserLoginInfo(request.Provider, request.ExternalUserId, request.Provider));
            }
        }

        var userDto = new UserDto()
        {
            Id = user!.Id,
            Name = $"{user.Name}".Trim(),
            UserName = user.UserName,
            Email = user.Email,
            Claims = _tokenClaimsService.GetUserClaims(user)
        };

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        response.SetAuthenticatedUser(userDto, principal);
        return response;
    }
}