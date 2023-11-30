namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    public class MarkSubscriptionPaymentAsPaidCommand : CommandBase
    {
        public MarkSubscriptionPaymentAsPaidCommand(Guid subscriptionPaymentId)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
        }

        public Guid SubscriptionPaymentId { get; }
    }
}