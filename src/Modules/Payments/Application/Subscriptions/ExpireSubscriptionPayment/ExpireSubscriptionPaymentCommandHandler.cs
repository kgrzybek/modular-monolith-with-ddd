using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayment
{
    internal class ExpireSubscriptionPaymentCommandHandler : ICommandHandler<ExpireSubscriptionPaymentCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public ExpireSubscriptionPaymentCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(ExpireSubscriptionPaymentCommand command, CancellationToken cancellationToken)
        {
            var subscriptionPayment = await _aggregateStore.Load(new SubscriptionPaymentId(command.PaymentId));

            subscriptionPayment.Expire();

            _aggregateStore.AppendChanges(subscriptionPayment);
        }
    }
}