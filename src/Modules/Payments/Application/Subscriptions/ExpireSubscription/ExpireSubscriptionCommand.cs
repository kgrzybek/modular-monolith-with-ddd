using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription
{
    public class ExpireSubscriptionCommand : InternalCommandBase
    {
        public Guid SubscriptionId { get; }

        public ExpireSubscriptionCommand(
            Guid id,
            Guid subscriptionId)
        : base(id)
        {
            SubscriptionId = subscriptionId;
        }
    }
}