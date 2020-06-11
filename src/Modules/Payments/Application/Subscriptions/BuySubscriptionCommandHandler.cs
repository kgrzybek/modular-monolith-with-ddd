using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions
{
    public class BuySubscriptionCommandHandler : ICommandHandler<BuySubscriptionCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        public BuySubscriptionCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(BuySubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = Subscription.Buy(
                new PayerId(command.PayerId),
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode);

            await _aggregateStore.Save(subscription);

            return Unit.Value;
        }
    }
}