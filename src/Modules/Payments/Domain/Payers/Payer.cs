using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public class Payer : AggregateRoot, IAggregateRoot
    {
        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private string _login;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createDate;

        public static Payer Create(
            Guid id,
            string login,
            string email,
            string firstName,
            string lastName,
            string name)
        {
            var payer = new Payer();

            var payerCreated = new PayerCreatedDomainEvent(
                id,
                login,
                firstName,
                lastName,
                name,
                email);

            payer.Apply(payerCreated);
            payer.AddDomainEvent(payerCreated);

            return payer;
        }

        private Payer()
        {
        }

        private void When(PayerCreatedDomainEvent @event)
        {
            this.Id = @event.PayerId;
            _login = @event.Login;
            _createDate = @event.OccurredOn;
            _email = @event.Email;
            _firstName = @event.FirstName;
            _lastName = @event.LastName;
            _name = @event.Name;
        }
    }
}