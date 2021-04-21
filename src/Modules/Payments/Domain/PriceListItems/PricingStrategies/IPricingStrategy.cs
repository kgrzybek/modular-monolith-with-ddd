using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies
{
    public interface IPricingStrategy
    {
        MoneyValue GetPrice(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category);
    }
}