using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationEvents
{
    public class SubscriptionExpirationDateChangedIntegrationEvent : IntegrationEvent
    {
        public SubscriptionExpirationDateChangedIntegrationEvent(
            Guid id,
            DateTime occurredOn,
            Guid payerId,
            DateTime expirationDate)
            : base(id, occurredOn)
        {
            PayerId = payerId;
            ExpirationDate = expirationDate;
        }

        public Guid PayerId { get; }

        public DateTime ExpirationDate { get; }
    }
}