using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceListItemData
    {
        public PriceListItemData(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            MoneyValue value,
            PriceListItemCategory category)
        {
            CountryCode = countryCode;
            Value = value;
            Category = category;
            SubscriptionPeriod = subscriptionPeriod;
        }

        public string CountryCode { get; }

        public SubscriptionPeriod SubscriptionPeriod { get; }

        public MoneyValue Value { get; }

        public PriceListItemCategory Category { get; }
    }
}