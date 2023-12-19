using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.GetAuthenticatorKey;

internal class GetAuthenticatorKeyQueryHandler : IQueryHandler<GetAuthenticatorKeyQuery, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAuthenticatorKeyQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(GetAuthenticatorKeyQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        // Try to get the authenticator key from the user
        var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);

        // if none is provided, which means none was generate before
        if (authenticatorKey == null)
        {
            // reset the authenticator key
            await _userManager.ResetAuthenticatorKeyAsync(user);

            // and get it again
            authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        if (!string.IsNullOrEmpty(authenticatorKey))
        {
            return Result<string>.Ok(authenticatorKey);
        }

        return Errors.Authentication.AuthenticatorKeyNotFound();
    }
}