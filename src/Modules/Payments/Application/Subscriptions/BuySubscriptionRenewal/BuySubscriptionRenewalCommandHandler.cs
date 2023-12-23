using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscriptionRenewal
{
    internal class BuySubscriptionRenewalCommandHandler : ICommandHandler<BuySubscriptionRenewalCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        private readonly IPayerContext _payerContext;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal BuySubscriptionRenewalCommandHandler(
            IAggregateStore aggregateStore,
            IPayerContext payerContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _aggregateStore = aggregateStore;
            _payerContext = payerContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(BuySubscriptionRenewalCommand command, CancellationToken cancellationToken)
        {
            var priceList = await PriceListFactory.CreatePriceList(_sqlConnectionFactory.GetOpenConnection());

            var subscriptionId = new SubscriptionId(command.SubscriptionId);

            var subscription = await _aggregateStore.Load(new SubscriptionId(command.SubscriptionId));

            if (subscription == null)
            {
                throw new InvalidCommandException(["Subscription for renewal must exist."]);
            }

            var subscriptionRenewalPayment = SubscriptionRenewalPayment.Buy(
                _payerContext.PayerId,
                subscriptionId,
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode,
                MoneyValue.Of(command.Value, command.Currency),
                priceList);

            _aggregateStore.AppendChanges(subscriptionRenewalPayment);

            return subscriptionRenewalPayment.Id;
        }
    }
}