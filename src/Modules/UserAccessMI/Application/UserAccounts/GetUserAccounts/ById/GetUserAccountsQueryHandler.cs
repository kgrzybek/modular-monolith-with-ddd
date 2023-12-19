using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.GetUserAccounts.ById;

internal class GetUserAccountsQueryHandler : IQueryHandler<GetUserAccountsQuery, Result<UserAccountDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserAccountsQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<UserAccountDto>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var userDto = new UserAccountDto()
        {
            Id = user.Id,
            AccessFailedCount = user.AccessFailedCount,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            Name = user.Name,
            FirstName = user.FirstName,
            LastName = user.LastName,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            UserName = user.UserName,
            NormalizedEmail = user.NormalizedEmail,
            NormalizedUserName = user.NormalizedUserName,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled
        };

        return Result<UserAccountDto>.Ok(userDto);
    }
}