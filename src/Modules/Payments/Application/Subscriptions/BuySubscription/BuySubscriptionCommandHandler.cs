using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;

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
            PriceList priceList = await GetPriceList();

            var subscription = SubscriptionPayment.Buy(
                _payerContext.PayerId,
                SubscriptionPeriod.Of(command.SubscriptionTypeCode),
                command.CountryCode,
                MoneyValue.Of(command.Value, command.Currency),
                priceList);
            
            _aggregateStore.AppendChanges(subscription);

            return subscription.Id;
        }

        private async Task<PriceList> GetPriceList()
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var priceListItems = await connection.QueryAsync<PriceListItemDto>("SELECT " +
                                                          $"[PriceListItem].[CountryCode] AS [{nameof(PriceListItemDto.CountryCode)}], " +
                                                          $"[PriceListItem].[SubscriptionPeriodCode] AS [{nameof(PriceListItemDto.SubscriptionPeriodCode)}], " +
                                                          $"[PriceListItem].[MoneyValue] AS [{nameof(PriceListItemDto.MoneyValue)}], " +
                                                          $"[PriceListItem].[MoneyCurrency] AS [{nameof(PriceListItemDto.MoneyCurrency)}] " +
                                                          "FROM [payments].[PriceListItems] AS [PriceListItem] " +
                                                          "WHERE [PriceListItem].[IsActive] = 1");

            var priceListItemList = priceListItems.AsList();

            return new PriceList(
                priceListItemList
                    .Select(x => 
                        new PriceListItem(
                            x.CountryCode, 
                            SubscriptionPeriod.Of(x.SubscriptionPeriodCode),
                            MoneyValue.Of(x.MoneyValue, x.MoneyCurrency)))
                    .ToList());
        }

        private class PriceListItemDto
        {
            public string CountryCode { get; set; }

            public string SubscriptionPeriodCode { get; set; }

            public decimal MoneyValue { get; set; }

            public string MoneyCurrency { get; set; }
        }
    }
}