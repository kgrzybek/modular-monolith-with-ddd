using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users.Events;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; private set; }

        private string _login;

        private string _password;

        private string _email;

#pragma warning disable CS0414 // Field is assigned but its value is never used
        private bool _isActive;
#pragma warning restore CS0414 // Field is assigned but its value is never used

        private string _firstName;

        private string _lastName;

        private string _name;

        private List<UserRole> _roles;

        private User()
        {
            // Only for EF.
        }

        public static User CreateAdmin(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string name)
        {
            return new User(
                Guid.NewGuid(),
                login,
                password,
                email,
                firstName,
                lastName,
                name,
                UserRole.Administrator);
        }

        public static User CreateUser(
            Guid userId,
            string login,
            string password,
            string email,
            string firstName,
            string lastName)
        {
            return new User(
                userId,
                login,
                password,
                email,
                firstName,
                lastName,
                $"{firstName} {lastName}",
                UserRole.Member);
        }

        private User(
            Guid id,
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string name,
            UserRole role)
        {
            this.Id = new UserId(id);
            _login = login;
            _password = password;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;

            _isActive = true;

            _roles = [role];

            this.AddDomainEvent(new UserCreatedDomainEvent(this.Id));
        }
    }
}