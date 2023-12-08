using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription
{
    public class ExpireSubscriptionCommand : InternalCommandBase
    {
        public Guid SubscriptionId { get; }

        [JsonConstructor]
        public ExpireSubscriptionCommand(
            Guid id,
            Guid subscriptionId)
        : base(id)
        {
            SubscriptionId = subscriptionId;
        }
    }
}