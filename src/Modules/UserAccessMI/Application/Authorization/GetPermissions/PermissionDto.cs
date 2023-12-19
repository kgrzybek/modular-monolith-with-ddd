namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authorization.GetPermissions;

public class PermissionDto
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}