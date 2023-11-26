using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription
{
    internal class ExpireSubscriptionCommandHandler : ICommandHandler<ExpireSubscriptionCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public ExpireSubscriptionCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(ExpireSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _aggregateStore.Load(new SubscriptionId(command.SubscriptionId));

            subscription.Expire();

            _aggregateStore.AppendChanges(subscription);
        }
    }
}