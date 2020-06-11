using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events
{
    public class SubscriptionPurchasedDomainEvent : DomainEventBase
    {
        public SubscriptionPurchasedDomainEvent(
            Guid subscriptionId, 
            Guid payerId, 
            string subscriptionPeriodCode, 
            string countryCode, 
            DateTime expirationDate)
        {
            SubscriptionId = subscriptionId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CountryCode = countryCode;
            ExpirationDate = expirationDate;
            PayerId = payerId;
        }

        public Guid PayerId { get; }

        public Guid SubscriptionId { get; }

        public string SubscriptionPeriodCode { get; }

        public string CountryCode { get; }

        public DateTime ExpirationDate { get; }
    }
}