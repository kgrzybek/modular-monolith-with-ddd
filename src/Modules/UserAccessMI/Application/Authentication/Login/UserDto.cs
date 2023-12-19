using System.Security.Claims;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

public class UserDto
{
    public Guid Id { get; init; }

    public string? UserName { get; init; } = null!;

    public string? Name { get; init; }

    public string? Email { get; init; }

    public List<Claim>? Claims { get; set; }
}