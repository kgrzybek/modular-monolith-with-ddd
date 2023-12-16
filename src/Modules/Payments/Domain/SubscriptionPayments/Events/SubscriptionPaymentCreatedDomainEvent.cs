using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events
{
    public class SubscriptionPaymentCreatedDomainEvent : DomainEventBase
    {
        public SubscriptionPaymentCreatedDomainEvent(
            Guid subscriptionPaymentId,
            Guid payerId,
            string subscriptionPeriodCode,
            string countryCode,
            string status,
            decimal value,
            string currency)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
            PayerId = payerId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CountryCode = countryCode;
            Status = status;
            Value = value;
            Currency = currency;
        }

        public Guid SubscriptionPaymentId { get; }

        public Guid PayerId { get; }

        public string SubscriptionPeriodCode { get; }

        public string CountryCode { get; }

        public string Status { get; }

        public decimal Value { get; }

        public string Currency { get; }
    }
}