using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Rules
{
    public class PriceForSubscriptionMustBeDefined : IBusinessRule
    {
        private readonly string _countryCode;

        private readonly SubscriptionPeriod _subscriptionPeriod;

        private readonly IList<PriceListItemData> _priceListItems;

        public PriceForSubscriptionMustBeDefined(
            string countryCode, 
            SubscriptionPeriod subscriptionPeriod, 
            IList<PriceListItemData> priceListItems)
        {
            _countryCode = countryCode;
            _subscriptionPeriod = subscriptionPeriod;
            _priceListItems = priceListItems;
        }

        public bool IsBroken() => _priceListItems.Count(x =>
            x.CountryCode == _countryCode &&
            x.SubscriptionPeriod == _subscriptionPeriod) != 1;

        public string Message => "Price for subscription must be defined";
    }
}