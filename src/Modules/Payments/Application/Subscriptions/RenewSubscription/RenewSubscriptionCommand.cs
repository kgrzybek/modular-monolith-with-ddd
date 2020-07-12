
using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class RenewSubscriptionCommand : InternalCommandBase
    {
        public Guid SubscriptionId { get; }

        public Guid SubscriptionRenewalPaymentId { get; }

        public RenewSubscriptionCommand(
            Guid id, Guid subscriptionId, Guid subscriptionRenewalPaymentId) : base(id)
        {
            SubscriptionId = subscriptionId;
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
        }
    }
}