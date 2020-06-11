using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events
{
    public class SubscriptionRenewedDomainEvent : DomainEventBase
    {
        public SubscriptionRenewedDomainEvent(
            Guid subscriptionId, 
            DateTime expirationDate, 
            string subscriptionPeriodCode)
        {
            SubscriptionId = subscriptionId;
            ExpirationDate = expirationDate;
            SubscriptionPeriodCode = subscriptionPeriodCode;
        }

        public Guid SubscriptionId { get; }

        public DateTime ExpirationDate { get; }

        public string SubscriptionPeriodCode { get; }
    }
}