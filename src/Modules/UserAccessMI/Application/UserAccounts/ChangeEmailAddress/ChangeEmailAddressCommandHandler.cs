using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.ChangeEmailAddress;

internal class ChangeEmailAddressCommandHandler : ICommandHandler<ChangeEmailAddressCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ChangeEmailAddressCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<Result<string>> Handle(ChangeEmailAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var result = await _userManager.ChangeEmailAsync(user, request.NewEmailAddress, request.Token);

        if (!result.Succeeded)
        {
            return result.Errors.Map().Combine();
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // Send confirmation email
        var emailMessage = new EmailMessage(
            request.NewEmailAddress,
            "MyMeetings - Confirm email address",
            $"Please confirm your email by calling the API with the provided token\n\nTOKEN: {token}");

        await _emailSender.SendEmail(emailMessage);
        return Result<string>.Ok(token);
    }
}