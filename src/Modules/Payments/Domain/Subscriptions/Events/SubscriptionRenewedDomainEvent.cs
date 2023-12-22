using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events
{
    public class SubscriptionRenewedDomainEvent : DomainEventBase
    {
        public SubscriptionRenewedDomainEvent(
            Guid subscriptionId,
            DateTime expirationDate,
            Guid payerId,
            string subscriptionPeriodCode,
            string status)
        {
            SubscriptionId = subscriptionId;
            ExpirationDate = expirationDate;
            PayerId = payerId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            Status = status;
        }

        public Guid SubscriptionId { get; }

        public DateTime ExpirationDate { get; }

        public Guid PayerId { get; }

        public string SubscriptionPeriodCode { get; }

        public string Status { get; }
    }
}