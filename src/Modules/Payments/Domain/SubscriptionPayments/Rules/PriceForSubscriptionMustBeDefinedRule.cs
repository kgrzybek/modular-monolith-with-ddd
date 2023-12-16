using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules
{
    public class PriceForSubscriptionMustBeDefinedRule : IBusinessRule
    {
        private readonly string _countryCode;

        private readonly SubscriptionPeriod _subscriptionPeriod;

        private readonly IList<PriceListItemData> _priceListItems;

        private readonly PriceListItemCategory _category;

        public PriceForSubscriptionMustBeDefinedRule(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            IList<PriceListItemData> priceListItems,
            PriceListItemCategory category)
        {
            _countryCode = countryCode;
            _subscriptionPeriod = subscriptionPeriod;
            _priceListItems = priceListItems;
            _category = category;
        }

        public bool IsBroken() => _priceListItems.Count(x =>
                                      x.CountryCode == _countryCode &&
                                      x.Category == _category &&
                                      x.SubscriptionPeriod == _subscriptionPeriod) != 1;

        public string Message => "Price for subscription must be defined";
    }
}