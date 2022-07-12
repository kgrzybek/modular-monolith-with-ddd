using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser
{
    public class AddAdminUserCommand : CommandBase
    {
        public AddAdminUserCommand(
            string login,
            string password,
            string firstName,
            string lastName,
            string name,
            string email)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            Email = email;
        }

        public string Login { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public string Email { get; }

        public string Password { get; }
    }
}