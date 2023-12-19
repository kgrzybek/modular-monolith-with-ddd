using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Queries;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.GetUserAccounts.Directory;

internal class GetUserAccountsQueryHandler : IQueryHandler<GetUserAccountsQuery, Result<IEnumerable<UserAccountDto>>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserAccountsQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<Result<IEnumerable<UserAccountDto>>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        var users = (from user in _userManager.Users
                     select new UserAccountDto()
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
                     }).ToList();

        return Task.FromResult(Result.Ok(users.AsEnumerable()));
    }
}