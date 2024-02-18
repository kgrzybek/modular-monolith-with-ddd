using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events
{
    public class NewUserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }

        public string Login { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public DateTime RegisterDate { get; }

        public string ConfirmLink { get; }

        public NewUserRegisteredDomainEvent(
            UserRegistrationId userRegistrationId,
            string login,
            string email,
            string firstName,
            string lastName,
            string name,
            DateTime registerDate,
            string confirmLink)
        {
            UserRegistrationId = userRegistrationId;
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            RegisterDate = registerDate;
            ConfirmLink = confirmLink;
        }
    }
}