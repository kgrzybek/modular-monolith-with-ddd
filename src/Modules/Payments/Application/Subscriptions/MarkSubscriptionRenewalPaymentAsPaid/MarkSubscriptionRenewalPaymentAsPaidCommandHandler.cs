using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid
{
    internal class MarkSubscriptionRenewalPaymentAsPaidCommandHandler
        : ICommandHandler<MarkSubscriptionRenewalPaymentAsPaidCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        internal MarkSubscriptionRenewalPaymentAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(MarkSubscriptionRenewalPaymentAsPaidCommand command, CancellationToken cancellationToken)
        {
            var subscriptionRenewalPayment =
                await _aggregateStore.Load(
                    new SubscriptionRenewalPaymentId(command.SubscriptionRenewalPaymentId));

            subscriptionRenewalPayment.MarkRenewalAsPaid();

            _aggregateStore.AppendChanges(subscriptionRenewalPayment);

            return Unit.Value;
        }
    }
}