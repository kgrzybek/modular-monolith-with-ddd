using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    internal class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        internal CreateSubscriptionCommandHandler(
            IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Guid> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscriptionPayment =
                await _aggregateStore.Load(new SubscriptionPaymentId(command.SubscriptionPaymentId));

            var subscription = Subscription.Create(subscriptionPayment.GetSnapshot());

            _aggregateStore.AppendChanges(subscription);

            return subscription.Id;
        }
    }
}