using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents
{
    public class NewUserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public string Login { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public NewUserRegisteredIntegrationEvent(Guid id, DateTime occurredOn, Guid userId, string login, string email, string firstName, string lastName, string name)
            : base(id, occurredOn)
        {
            UserId = userId;
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
        }
    }
}
