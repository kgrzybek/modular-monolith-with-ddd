using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments
{
    public class PriceListItem
    {
        public PriceListItem(
            string countryCode, SubscriptionPeriod subscriptionPeriod, MoneyValue value)
        {
            CountryCode = countryCode;
            Value = value;
            SubscriptionPeriod = subscriptionPeriod;
        }

        public string CountryCode { get; }

        public SubscriptionPeriod SubscriptionPeriod { get; }

        public MoneyValue Value { get; }
    }
}