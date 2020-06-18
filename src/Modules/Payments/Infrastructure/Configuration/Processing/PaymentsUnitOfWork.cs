using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing
{
    public class PaymentsUnitOfWork : IUnitOfWork
    {
        private readonly IOutbox _outbox;

        private readonly IAggregateStore _aggregateStore;

        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public PaymentsUnitOfWork(
            IOutbox outbox, 
            IAggregateStore aggregateStore, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _outbox = outbox;
            _aggregateStore = aggregateStore;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventsDispatcher.DispatchEventsAsync();

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _aggregateStore.Save();

            await _outbox.Save();

            transaction.Complete();

            return 0;
        }
    }
}