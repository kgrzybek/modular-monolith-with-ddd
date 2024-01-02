using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser;

public class CreateUserCommand : CommandBase
{
    public CreateUserCommand(
        Guid userId,
        string login,
        string email,
        string firstName,
        string lastName,
        string name,
        string password)
    {
        UserId = userId;
        Login = login;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;
        Password = password;
    }

    public Guid UserId { get; }

    public string Login { get; }

    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Name { get; }

    public string Password { get; }
}