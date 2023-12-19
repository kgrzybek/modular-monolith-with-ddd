namespace CompanyName.MyMeetings.Contracts.V1.Users.Identity;

public class UserAccountDto
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public string UserName { get; set; } = null!;

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }
}