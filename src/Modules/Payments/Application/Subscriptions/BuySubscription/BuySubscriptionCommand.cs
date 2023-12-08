using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription
{
    public class BuySubscriptionCommand : CommandBase<Guid>
    {
        public BuySubscriptionCommand(
            string subscriptionTypeCode,
            string countryCode,
            decimal value,
            string currency)
        {
            SubscriptionTypeCode = subscriptionTypeCode;
            CountryCode = countryCode;
            Value = value;
            Currency = currency;
        }

        public string SubscriptionTypeCode { get; }

        public string CountryCode { get; }

        public decimal Value { get; }

        public string Currency { get; }
    }
}