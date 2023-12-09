using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class SubscriptionRenewedNotification : DomainNotificationBase<SubscriptionRenewedDomainEvent>
    {
        [JsonConstructor]
        public SubscriptionRenewedNotification(SubscriptionRenewedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}