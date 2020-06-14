using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    internal static class DomainEventTypeMappings
    {
        internal static IDictionary<string, Type> Dictionary { get; }

        static DomainEventTypeMappings()
        {
            Dictionary = new Dictionary<string, Type>();
            Dictionary.Add("SubscriptionPurchased", typeof(SubscriptionPurchasedDomainEvent));
            Dictionary.Add("SubscriptionRenewed", typeof(SubscriptionRenewedDomainEvent));
            Dictionary.Add("SubscriptionExpired", typeof(SubscriptionExpiredDomainEvent));
        }
    }
}