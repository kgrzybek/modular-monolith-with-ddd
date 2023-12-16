using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events
{
    public class SubscriptionExpiredDomainEvent : DomainEventBase
    {
        public SubscriptionExpiredDomainEvent(
            Guid subscriptionId,
            string status)
        {
            SubscriptionId = subscriptionId;
            Status = status;
        }

        public Guid SubscriptionId { get; }

        public string Status { get; }
    }
}