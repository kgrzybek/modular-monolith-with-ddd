using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    public class MarkSubscriptionPaymentAsPaidCommandHandler : ICommandHandler<MarkSubscriptionPaymentAsPaidCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public MarkSubscriptionPaymentAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(MarkSubscriptionPaymentAsPaidCommand command, CancellationToken cancellationToken)
        {
            var subscriptionPayment =
                await _aggregateStore.Load(new SubscriptionPaymentId(command.SubscriptionPaymentId));

            subscriptionPayment.MarkAsPaid();

            await _aggregateStore.Save(subscriptionPayment);

            return Unit.Value;
        }
    }
}