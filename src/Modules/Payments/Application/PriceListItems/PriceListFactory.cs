using System.Data;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems
{
    public static class PriceListFactory
    {
        public static async Task<PriceList> CreatePriceList(IDbConnection connection)
        {
            var priceListItemList = await GetPriceListItems(connection);

            var priceListItems = priceListItemList
                .Select(x =>
                    new PriceListItemData(
                        x.CountryCode,
                        SubscriptionPeriod.Of(x.SubscriptionPeriodCode),
                        MoneyValue.Of(x.MoneyValue, x.MoneyCurrency),
                        PriceListItemCategory.Of(x.CategoryCode)))
                .ToList();

            // This is place for selecting pricing strategy based on provided data and the system state.
            IPricingStrategy pricingStrategy = new DirectValueFromPriceListPricingStrategy(priceListItems);

            return PriceList.Create(
                priceListItems,
                pricingStrategy);
        }

        public static async Task<List<PriceListItemDto>> GetPriceListItems(IDbConnection connection)
        {
            const string sql = $"""
                                SELECT
                                    [PriceListItem].[CountryCode] AS [{nameof(PriceListItemDto.CountryCode)}],
                                    [PriceListItem].[SubscriptionPeriodCode] AS [{nameof(PriceListItemDto.SubscriptionPeriodCode)}],
                                    [PriceListItem].[MoneyValue] AS [{nameof(PriceListItemDto.MoneyValue)}],
                                    [PriceListItem].[MoneyCurrency] AS [{nameof(PriceListItemDto.MoneyCurrency)}],
                                    [PriceListItem].[CategoryCode] AS [{nameof(PriceListItemDto.CategoryCode)}]
                                FROM [payments].[PriceListItems] AS [PriceListItem]
                                WHERE [PriceListItem].[IsActive] = 1
                                """;
            var priceListItems = await connection.QueryAsync<PriceListItemDto>(
                sql);

            var priceListItemList = priceListItems.AsList();
            return priceListItemList;
        }
    }
}