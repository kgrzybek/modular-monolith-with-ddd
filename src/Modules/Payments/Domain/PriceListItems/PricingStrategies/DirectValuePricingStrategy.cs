using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies
{
    public class DirectValuePricingStrategy : IPricingStrategy
    {
        private readonly MoneyValue _directValue;

        public DirectValuePricingStrategy(MoneyValue directValue)
        {
            _directValue = directValue;
        }

        public MoneyValue GetPrice(string countryCode, SubscriptionPeriod subscriptionPeriod, PriceListItemCategory category)
        {
            return _directValue;
        }
    }
}