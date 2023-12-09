using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription
{
    public class RenewSubscriptionCommand : InternalCommandBase
    {
        public Guid SubscriptionId { get; }

        public Guid SubscriptionRenewalPaymentId { get; }

        [JsonConstructor]
        public RenewSubscriptionCommand(
            Guid id, Guid subscriptionId, Guid subscriptionRenewalPaymentId)
            : base(id)
        {
            SubscriptionId = subscriptionId;
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
        }
    }
}