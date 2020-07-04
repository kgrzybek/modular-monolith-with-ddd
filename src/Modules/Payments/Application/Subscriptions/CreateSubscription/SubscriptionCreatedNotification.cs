using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class SubscriptionCreatedNotification : DomainNotificationBase<SubscriptionPurchasedDomainEvent>
    {
        [JsonConstructor]
        protected SubscriptionCreatedNotification(SubscriptionPurchasedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}