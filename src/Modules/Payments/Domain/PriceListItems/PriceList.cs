using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceList : ValueObject
    {
        private readonly List<PriceListItemData> _items;

        private PriceList(List<PriceListItemData> items)
        {
            _items = items;
        }

        public static PriceList CreateFromItems(List<PriceListItemData> items)
        {
            return new PriceList(items);
        }

        public MoneyValue GetPrice(
            string countryCode, 
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category)
        {
            CheckRule(new PriceForSubscriptionMustBeDefined(countryCode, subscriptionPeriod, _items, category));
            
            var priceListItem = _items.Single(x => 
                x.CountryCode == countryCode && x.SubscriptionPeriod == subscriptionPeriod &&
                x.Category == category);

            return priceListItem.Value;
        }
    }
}