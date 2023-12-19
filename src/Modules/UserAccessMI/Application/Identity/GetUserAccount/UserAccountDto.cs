namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Identity.GetUserAccount;

public class UserAccountDto
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public string? UserName { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }
}