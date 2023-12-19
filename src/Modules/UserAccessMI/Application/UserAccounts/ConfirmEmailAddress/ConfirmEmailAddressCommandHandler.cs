using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ConfirmEmailAddress;

internal class ConfirmEmailAddressCommandHandler : ICommandHandler<ConfirmEmailAddressCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailAddressCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ConfirmEmailAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Errors.General.NotFound(request.EmailAddress, "User");
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
        {
            return result.Errors.Map().Combine();
        }

        return Result.Ok();
    }
}