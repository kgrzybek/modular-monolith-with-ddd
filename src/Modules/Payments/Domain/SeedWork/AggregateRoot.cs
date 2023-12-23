using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents;

        public Guid Id { get; protected set; }

        public int Version { get; private set; }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        protected AggregateRoot()
        {
            _domainEvents = [];

            Version = -1;
        }

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