using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Configuration.Identity;

public class DoesNotContainPasswordValidator<TUser> : IPasswordValidator<TUser>
    where TUser : class
{
    public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
    {
        if (password is null)
        {
            return IdentityResult.Success;
        }

        var username = await manager.GetUserNameAsync(user);

        if (username == password)
        {
            return IdentityResult.Failed(new IdentityError() { Description = "Password cannot contain username." });
        }

        if (password.Contains("password"))
        {
            return IdentityResult.Failed(new IdentityError() { Description = "Password cannot contain password." });
        }

        return IdentityResult.Success;
    }
}