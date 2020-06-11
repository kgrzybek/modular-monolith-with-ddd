using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriptionPurchasedDomainEvent : DomainEventBase
    {
        public SubscriptionPurchasedDomainEvent(
            Guid subscriptionId, 
            Guid payerId, 
            string subscriptionPeriodCode, string countryCode)
        {
            SubscriptionId = subscriptionId;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CountryCode = countryCode;
            PayerId = payerId;
        }

        public Guid PayerId { get; }

        public Guid SubscriptionId { get; }

        public string SubscriptionPeriodCode { get; }

        public string CountryCode { get; }
    }
}