using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommand : CommandBase<Guid>
    {
        public RegisterNewUserCommand(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string confirmLink)
        {
            Login = login;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            ConfirmLink = confirmLink;
        }

        public string Login { get; }

        public string Password { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string ConfirmLink { get; }
    }
}