using System.Transactions;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing
{
    public class PaymentsUnitOfWork : IUnitOfWork
    {
        private readonly IOutbox _outbox;

        private readonly IAggregateStore _aggregateStore;

        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public PaymentsUnitOfWork(
            IOutbox outbox,
            IAggregateStore aggregateStore,
            IDomainEventsDispatcher domainEventsDispatcher,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _outbox = outbox;
            _aggregateStore = aggregateStore;
            _domainEventsDispatcher = domainEventsDispatcher;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<int> CommitAsync(
            CancellationToken cancellationToken = default,
            Guid? internalCommandId = null)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();

            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using var transaction = new TransactionScope(
                TransactionScopeOption.Required,
                options,
                TransactionScopeAsyncFlowOption.Enabled);

            await _aggregateStore.Save();

            await _outbox.Save();

            if (internalCommandId.HasValue)
            {
                using var connection = _sqlConnectionFactory.CreateNewConnection();
                await connection.ExecuteScalarAsync(
                    "UPDATE payments.InternalCommands " +
                    "SET ProcessedDate = @Date " +
                    "WHERE Id = @Id",
                    new
                    {
                        Date = DateTime.UtcNow,
                        Id = internalCommandId.Value
                    });
            }

            transaction.Complete();

            return 0;
        }
    }
}