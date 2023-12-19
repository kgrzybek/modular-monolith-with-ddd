namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.GetUserRegistration;

public class UserRegistrationDto
{
    public Guid Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string StatusCode { get; set; } = null!;
}