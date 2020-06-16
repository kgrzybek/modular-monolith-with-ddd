using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments
{
    public class PriceList : ValueObject
    {
        private readonly List<PriceListItem> _items;

        public PriceList(List<PriceListItem> items)
        {
            _items = items;
        }

        public MoneyValue GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod)
        {
            CheckRule(new PriceForSubscriptionMustBeDefined(countryCode, subscriptionPeriod, _items));
            
            var priceListItem = _items.Single(x => 
                x.CountryCode == countryCode && x.SubscriptionPeriod == subscriptionPeriod);

            return priceListItem.Value;
        }
    }
}