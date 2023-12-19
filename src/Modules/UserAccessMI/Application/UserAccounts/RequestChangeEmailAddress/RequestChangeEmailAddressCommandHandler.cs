using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.RequestChangeEmailAddress;

internal class RequestChangeEmailAddressCommandHandler : ICommandHandler<RequestChangeEmailAddressCommand, RequestChangeEmailAddressResult>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RequestChangeEmailAddressCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RequestChangeEmailAddressResult> Handle(RequestChangeEmailAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user != null)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmailAddress);
            return new RequestChangeEmailAddressResult(token);
        }

        var response = new RequestChangeEmailAddressResult();
        response.AddError(Errors.General.NotFound(request.UserId, "User"));

        return response;
    }
}