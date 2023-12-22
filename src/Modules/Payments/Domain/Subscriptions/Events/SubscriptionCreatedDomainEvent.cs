using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events
{
    public class SubscriptionCreatedDomainEvent : DomainEventBase
    {
        public SubscriptionCreatedDomainEvent(
            Guid subscriptionPaymentId,
            Guid subscriptionId,
            Guid payerId,
            string subscriptionPeriodCode,
            string countryCode,
            DateTime expirationDate,
            string status)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
            SubscriptionId = subscriptionId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CountryCode = countryCode;
            ExpirationDate = expirationDate;
            Status = status;
            PayerId = payerId;
        }

        public Guid PayerId { get; }

        public Guid SubscriptionPaymentId { get; }

        public Guid SubscriptionId { get; }

        public string SubscriptionPeriodCode { get; }

        public string CountryCode { get; }

        public DateTime ExpirationDate { get; }

        public string Status { get; }
    }
}