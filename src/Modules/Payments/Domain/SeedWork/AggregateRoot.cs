using System;
using System.Collections.Generic;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        public int VersionId { get; private set; }

        private readonly List<IDomainEvent> _domainEvents;

        protected AggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();

            VersionId = -1;
        }

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();
    }
}