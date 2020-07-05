using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems
{
    public static class PriceListProvider
    {
        public static async Task<PriceList> GetPriceList(IDbConnection connection)
        {
            var priceListItemList = await GetPriceListItems(connection);

            return new PriceList(
                priceListItemList
                    .Select(x =>
                        new PriceListItemData(
                            x.CountryCode,
                            SubscriptionPeriod.Of(x.SubscriptionPeriodCode),
                            MoneyValue.Of(x.MoneyValue, x.MoneyCurrency),
                            PriceListItemCategory.Of(x.CategoryCode)))
                    .ToList());
        }

        public static async Task<List<PriceListItemDto>> GetPriceListItems(IDbConnection connection)
        {
            var priceListItems = await connection.QueryAsync<PriceListItemDto>("SELECT " +
                                                                               $"[PriceListItem].[CountryCode] AS [{nameof(PriceListItemDto.CountryCode)}], " +
                                                                               $"[PriceListItem].[SubscriptionPeriodCode] AS [{nameof(PriceListItemDto.SubscriptionPeriodCode)}], " +
                                                                               $"[PriceListItem].[MoneyValue] AS [{nameof(PriceListItemDto.MoneyValue)}], " +
                                                                               $"[PriceListItem].[MoneyCurrency] AS [{nameof(PriceListItemDto.MoneyCurrency)}], " +
                                                                               $"[PriceListItem].[CategoryCode] AS [{nameof(PriceListItemDto.CategoryCode)}] " +
                                                                               "FROM [payments].[PriceListItems] AS [PriceListItem] " +
                                                                               "WHERE [PriceListItem].[IsActive] = 1");

            var priceListItemList = priceListItems.AsList();
            return priceListItemList;
        }
    }
}