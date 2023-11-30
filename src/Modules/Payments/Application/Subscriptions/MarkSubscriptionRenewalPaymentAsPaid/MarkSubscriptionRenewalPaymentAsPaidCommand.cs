namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid
{
    public class MarkSubscriptionRenewalPaymentAsPaidCommand : CommandBase
    {
        public Guid SubscriptionRenewalPaymentId { get; }

        public MarkSubscriptionRenewalPaymentAsPaidCommand(Guid subscriptionRenewalPaymentId)
        {
            SubscriptionRenewalPaymentId = subscriptionRenewalPaymentId;
        }
    }
}