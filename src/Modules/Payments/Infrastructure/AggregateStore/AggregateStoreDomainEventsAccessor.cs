using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class AggregateStoreDomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly IAggregateStore _aggregateStore;

        public AggregateStoreDomainEventsAccessor(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            return _aggregateStore
                .GetChanges()
                .ToList()
                .AsReadOnly();
        }

        public void ClearAllDomainEvents()
        {
            _aggregateStore.ClearChanges();
        }
    }
}