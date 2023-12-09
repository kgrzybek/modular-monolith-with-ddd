using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscriptionRenewal
{
    public class BuySubscriptionRenewalCommand : CommandBase<Guid>
    {
        public BuySubscriptionRenewalCommand(
            Guid subscriptionId,
            string subscriptionTypeCode,
            string countryCode,
            decimal value,
            string currency)
        {
            SubscriptionId = subscriptionId;
            SubscriptionTypeCode = subscriptionTypeCode;
            CountryCode = countryCode;
            Value = value;
            Currency = currency;
        }

        public Guid SubscriptionId { get; }

        public string SubscriptionTypeCode { get; }

        public string CountryCode { get; }

        public decimal Value { get; }

        public string Currency { get; }
    }
}