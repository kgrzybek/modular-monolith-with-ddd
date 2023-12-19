using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;

public class Role : IdentityRole<Guid>
{
    public Role()
        : base()
    {
    }

    public Role(string roleName)
        : base(roleName)
    {
    }
}