using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events
{
    public class SubscriptionRenewalPaymentCreatedDomainEvent : DomainEventBase
    {
        public SubscriptionRenewalPaymentCreatedDomainEvent(
            Guid subscriptionRenewalPaymentId,
            Guid payerId,
            Guid subscriptionId,
            string subscriptionPeriodCode,
            string countryCode,
            string status,
            decimal value,
            string currency)
        {
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
            PayerId = payerId;
            SubscriptionId = subscriptionId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CountryCode = countryCode;
            Status = status;
            Value = value;
            Currency = currency;
        }

        public Guid SubscriptionRenewalPaymentId { get; }

        public Guid PayerId { get; }

        public Guid SubscriptionId { get; }

        public string SubscriptionPeriodCode { get; }

        public string CountryCode { get; }

        public string Status { get; }

        public decimal Value { get; }

        public string Currency { get; }
    }
}