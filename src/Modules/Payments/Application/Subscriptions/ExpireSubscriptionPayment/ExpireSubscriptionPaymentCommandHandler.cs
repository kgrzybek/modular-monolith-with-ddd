using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayment
{
    public class ExpireSubscriptionPaymentCommandHandler : ICommandHandler<ExpireSubscriptionPaymentCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public ExpireSubscriptionPaymentCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(ExpireSubscriptionPaymentCommand command, CancellationToken cancellationToken)
        {
            var subscriptionPayment = await _aggregateStore.Load(new SubscriptionPaymentId(command.PaymentId));

            subscriptionPayment.Expire();

            _aggregateStore.AppendChanges(subscriptionPayment);

            return Unit.Value;
        }
    }
}