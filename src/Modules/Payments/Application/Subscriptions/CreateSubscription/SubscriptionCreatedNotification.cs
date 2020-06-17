using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class SubscriptionCreatedNotification : DomainNotificationBase<SubscriptionCreatedDomainEvent>
    {
        protected SubscriptionCreatedNotification(SubscriptionCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}