using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    public class MarkSubscriptionPaymentAsPaidCommand : CommandBase<Unit>
    {
        public MarkSubscriptionPaymentAsPaidCommand(Guid subscriptionPaymentId)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
        }

        public Guid SubscriptionPaymentId { get; }
    }
}