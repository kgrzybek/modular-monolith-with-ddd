using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers
{
    public class Payer : Entity, IAggregateRoot
    {
        public PayerId Id { get; private set; }

        private string _login;

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _name;

        private DateTime _createDate;

        private Payer()
        {
            // For EF.
        }

        public static Payer Create(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            return new Payer(id, login, email, firstName, lastName, name);
        }

        private Payer(Guid id, string login, string email, string firstName, string lastName, string name)
        {
            this.Id = new PayerId(id);
            _login = login;
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _name = name;
            _createDate = DateTime.UtcNow;

            this.AddDomainEvent(new PayerCreatedDomainEvent(this.Id));
        }
    }
}