namespace CompanyName.MyMeetings.Contracts.V1.Users.Roles;

public class SetRolePermissionsRequest
{
    public string[] Permissions { get; set; } = null!;
}