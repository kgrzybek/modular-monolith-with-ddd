using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events
{
    public class PayerCreatedDomainEvent : DomainEventBase
    {
        public Guid PayerId { get; }

        public string Login { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public string Email { get; }

        public PayerCreatedDomainEvent(
            Guid payerId,
            string login,
            string firstName,
            string lastName,
            string name,
            string email)
        {
            PayerId = payerId;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Name = name;
            Email = email;
        }
    }
}