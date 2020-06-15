using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription
{
    public class BuySubscriptionCommandHandler : ICommandHandler<BuySubscriptionCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        private readonly IPayerContext _payerContext;

        public BuySubscriptionCommandHandler(
            IAggregateStore aggregateStore, 
            IPayerContext payerContext)
        {
            _aggregateStore = aggregateStore;
            _payerContext = payerContext;
        }

        public async Task<Guid> Handle(BuySubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = Subscription.Buy(
                _payerContext.PayerId,
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode);

            await _aggregateStore.Save(subscription);

            return subscription.Id;
        }
    }
}