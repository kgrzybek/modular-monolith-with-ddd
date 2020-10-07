﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription
{
    public class BuySubscriptionCommandHandler : ICommandHandler<BuySubscriptionCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        private readonly IPayerContext _payerContext;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public BuySubscriptionCommandHandler(
            IAggregateStore aggregateStore,
            IPayerContext payerContext,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _aggregateStore = aggregateStore;
            _payerContext = payerContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(BuySubscriptionCommand command, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();
            
            var priceList = await PriceListProvider.GetPriceList(connection);

            var subscription = SubscriptionPayment.Buy(
                _payerContext.PayerId,
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode,
                MoneyValue.Of(command.Value, command.Currency),
                priceList);

            _aggregateStore.AppendChanges(subscription);

            return subscription.Id;
        }
    }
}