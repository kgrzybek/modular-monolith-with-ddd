using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments
{
    public class SubscriptionRenewalPaymentSnapshot
    {
        public SubscriptionRenewalPaymentSnapshot(
            SubscriptionRenewalPaymentId id,
            PayerId payerId,
            SubscriptionPeriod subscriptionPeriod,
            string countryCode)
        {
            PayerId = payerId;
            SubscriptionPeriod = subscriptionPeriod;
            CountryCode = countryCode;
            Id = id;
        }

        public PayerId PayerId { get; }

        public SubscriptionPeriod SubscriptionPeriod { get; }

        public string CountryCode { get; }

        public SubscriptionRenewalPaymentId Id { get; }
    }
}