using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    internal class MarkSubscriptionPaymentAsPaidCommandHandler : ICommandHandler<MarkSubscriptionPaymentAsPaidCommand, Unit>
    {
        private readonly IAggregateStore _aggregateStore;

        internal MarkSubscriptionPaymentAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(MarkSubscriptionPaymentAsPaidCommand command, CancellationToken cancellationToken)
        {
            var subscriptionPayment =
                await _aggregateStore.Load(new SubscriptionPaymentId(command.SubscriptionPaymentId));

            subscriptionPayment.MarkAsPaid();

            _aggregateStore.AppendChanges(subscriptionPayment);

            return Unit.Value;
        }
    }
}