namespace CompanyName.MyMeetings.Contracts.V1.Users.Users;

public class SetUserRolesRequest
{
    public Guid[] RoleIds { get; set; } = null!;
}