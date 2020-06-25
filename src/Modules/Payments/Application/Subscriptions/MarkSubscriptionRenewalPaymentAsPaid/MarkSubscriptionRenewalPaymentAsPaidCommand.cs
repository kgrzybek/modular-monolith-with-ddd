using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid
{
    public class MarkSubscriptionRenewalPaymentAsPaidCommand : CommandBase<Unit>
    {
        public Guid SubscriptionRenewalPaymentId { get; }

        public MarkSubscriptionRenewalPaymentAsPaidCommand(Guid subscriptionRenewalPaymentId)
        {
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
        }
    }
}