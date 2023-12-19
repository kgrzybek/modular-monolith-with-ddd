namespace CompanyName.MyMeetings.Contracts.V1.Users.Roles;

public class AddRoleRequest
{
    public string Name { get; set; } = null!;

    public string[] Permissions { get; set; } = null!;
}
