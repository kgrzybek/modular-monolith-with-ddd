using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.SendSubscriptionCreationConfirmationEmail
{
    public class SendSubscriptionCreationConfirmationEmailCommand : InternalCommandBase
    {
        internal SubscriptionId SubscriptionId { get; }

        internal string Email { get; }

        [JsonConstructor]
        public SendSubscriptionCreationConfirmationEmailCommand(
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