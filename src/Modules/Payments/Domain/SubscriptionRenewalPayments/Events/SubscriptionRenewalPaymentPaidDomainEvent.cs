using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events
{
    public class SubscriptionRenewalPaymentPaidDomainEvent : DomainEventBase
    {
        public SubscriptionRenewalPaymentPaidDomainEvent(
            Guid subscriptionRenewalPaymentId, Guid subscriptionId, string status)
        {
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
            SubscriptionId = subscriptionId;
            Status = status;
        }

        public Guid SubscriptionRenewalPaymentId { get; }

        public Guid SubscriptionId { get; }

        public string Status { get; }
    }
}