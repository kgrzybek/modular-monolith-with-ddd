using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        public int Version { get; private set; }

        private readonly List<IDomainEvent> _domainEvents;

        protected AggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();

            Version = -1;
        }

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        public void Load(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history)
            {
                Apply(e);
                Version++;
            }
        }

        protected abstract void Apply(IDomainEvent @event);

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}