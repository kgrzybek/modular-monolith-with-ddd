using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionRenewalConfirmationEmail
{
    public class SendSubscriptionRenewalConfirmationEmailCommand : InternalCommandBase
    {
        internal SubscriptionId SubscriptionId { get; }

        internal string Email { get; }

        [JsonConstructor]
        public SendSubscriptionRenewalConfirmationEmailCommand(
            Guid id,
            SubscriptionId subscriptionId,
            string email)
            : base(id)
        {
            SubscriptionId = subscriptionId;
            Email = email;
        }
    }
}