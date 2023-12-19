using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.CreateUserAccount;

internal class CreateUserAccountCommandHandler : ICommandHandler<CreateUserAccountCommand, Result<Guid>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public CreateUserAccountCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _emailSender = emailSender;
        _userManager = userManager;
    }

    public async Task<Result<Guid>> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        // If user wasn't found, go ahead and create the new user.
        // But if user exists don't tell anyone. -> Protection against account enumeration
        if (user == null)
        {
            user = new ApplicationUser(request.UserName)
            {
                Email = request.EmailAddress,
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                return identityResult.Errors.Map().Combine();
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordResult = await _userManager.AddPasswordAsync(user, request.Password);
                if (!passwordResult.Succeeded)
                {
                    return passwordResult.Errors.Map().Combine();
                }
            }

            return Result.Ok(user.Id);
        }
        else
        {
            // Send email to user informing them that he already have an account.
            // This way they can pro actively go out and use the forgot password functionality and reclaim there account.
            var emailMessage = new EmailMessage(
                user.Email,
                "MyMeetings - Create user account",
                $"An account with the provided email address {user.Email} already exists. Please use the forgot password functionality to reclaim your account.");
            await _emailSender.SendEmail(emailMessage);
        }

        return Result.Ok<Guid>();
    }
}