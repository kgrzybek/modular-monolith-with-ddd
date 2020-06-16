using System;
using System.Collections.Generic;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    internal static class DomainEventTypeMappings
    {
        internal static IDictionary<string, Type> Dictionary { get; }

        static DomainEventTypeMappings()
        {
            Dictionary = new Dictionary<string, Type>();
            Dictionary.Add("SubscriptionPaymentCreated", typeof(SubscriptionPaymentCreatedDomainEvent));
            Dictionary.Add("SubscriptionPaymentPaid", typeof(SubscriptionPaymentPaidDomainEvent));

            Dictionary.Add("SubscriptionCreated", typeof(SubscriptionCreatedDomainEvent));
            Dictionary.Add("SubscriptionRenewed", typeof(SubscriptionRenewedDomainEvent));
            Dictionary.Add("SubscriptionExpired", typeof(SubscriptionExpiredDomainEvent));
        }
    }
}