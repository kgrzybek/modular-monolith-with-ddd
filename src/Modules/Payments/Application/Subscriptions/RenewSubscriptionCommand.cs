using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions
{
    public class RenewSubscriptionCommand : CommandBase
    {
        public Guid SubscriptionId { get; }

        public string SubscriptionTypeCode { get; }

        public RenewSubscriptionCommand(
            Guid subscriptionId, 
            string subscriptionTypeCode)
        {
            SubscriptionTypeCode = subscriptionTypeCode;
            SubscriptionId = subscriptionId;
        }
    }
}