using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription
{
    public class ExpireSubscriptionCommand : CommandBase
    {
        public Guid SubscriptionId { get; }

        public ExpireSubscriptionCommand(
            Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
    }
}