using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RequestForgotPasswordLink;

internal class RequestForgotPasswordLinkCommandHandler : ICommandHandler<RequestForgotPasswordLinkCommand, ForgotPasswordLinkResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public RequestForgotPasswordLinkCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<ForgotPasswordLinkResult> Handle(RequestForgotPasswordLinkCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.EmailAddress);
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new ForgotPasswordLinkResult(token);
        }

        // email user and inform them that they do not have an account with that email address
        var message = new EmailMessage(
                request.EmailAddress,
                "MyMeetings - Forgot password",
                $"We could not find your account with the given email addess '{request.EmailAddress}'.\n\nPlease check if you registered with an different email address.");

        await _emailSender.SendEmail(message);

        var response = new ForgotPasswordLinkResult();
        response.AddError(Errors.General.NotFound(request.EmailAddress, "User"));
        return response;
    }
}