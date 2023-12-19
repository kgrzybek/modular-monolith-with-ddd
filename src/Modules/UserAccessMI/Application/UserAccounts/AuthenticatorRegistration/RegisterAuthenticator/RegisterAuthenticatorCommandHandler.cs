using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.AuthenticatorRegistration.RegisterAuthenticator;

internal class RegisterAuthenticatorCommandHandler : ICommandHandler<RegisterAuthenticatorCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterAuthenticatorCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(RegisterAuthenticatorCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.OtpCode);
        if (!isValid)
        {
            return Errors.Authentication.InvalidTwoFactorAuthenticationToken();
        }

        await _userManager.SetTwoFactorEnabledAsync(user, true);
        return Result.Ok();
    }
}