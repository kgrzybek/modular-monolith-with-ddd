using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserAccount;

internal class GetUserAccountQueryHandler : IQueryHandler<GetUserAccountQuery, Result<UserAccountDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserAccountQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<UserAccountDto>> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "user");
        }

        return new UserAccountDto
        {
            Id = user.Id,
            IsActive = user.LockoutEnd is null,
            EmailAddress = user.Email,
            UserName = user.UserName,
            Name = user.Name,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}