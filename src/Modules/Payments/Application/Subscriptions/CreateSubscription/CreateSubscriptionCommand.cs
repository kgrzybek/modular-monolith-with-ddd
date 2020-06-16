using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription
{
    public class CreateSubscriptionCommand : CommandBase<Guid>
    {
        public Guid SubscriptionPaymentId { get; }

        public CreateSubscriptionCommand(
            Guid subscriptionPaymentId)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
        }
    }
}