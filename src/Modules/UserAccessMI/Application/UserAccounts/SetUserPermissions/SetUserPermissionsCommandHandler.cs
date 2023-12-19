using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserAccounts.SetUserPermissions;

internal class SetUserPermissionsCommandHandler : ICommandHandler<SetUserPermissionsCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SetUserPermissionsCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(SetUserPermissionsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Errors.General.NotFound(request.UserId, "User");
        }

        var permissions = request.Permissions ?? Enumerable.Empty<string>();

        var userClaims = (await _userManager.GetClaimsAsync(user)) ?? Enumerable.Empty<Claim>();
        var permissionsToAdd = permissions.ExceptBy(userClaims.Select(x => x.Value), y => y).ToList();
        var claimsToRemove = userClaims.ExceptBy(permissions, y => y.Value).ToList();

        if (permissionsToAdd.Any())
        {
            await _userManager.AddClaimsAsync(user, permissionsToAdd.Select(x => new Claim(CustomClaimTypes.Permission, x)).ToArray());
            user = await _userManager.FindByIdAsync(user.Id.ToString());
        }

        if (claimsToRemove.Any())
        {
            await _userManager.RemoveClaimsAsync(user!, claimsToRemove);
            user = await _userManager.FindByIdAsync(user!.Id.ToString());
        }

        return Result.Ok();
    }
}