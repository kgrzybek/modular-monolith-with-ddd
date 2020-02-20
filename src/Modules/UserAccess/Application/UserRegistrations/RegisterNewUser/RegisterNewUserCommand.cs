using System;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommand : CommandBase<Guid>
    {
        public RegisterNewUserCommand(string login, string password, string email, string firstName, string lastName)
        {
            Login = login;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Login { get; }

        public string Password { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}