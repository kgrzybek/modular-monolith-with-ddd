using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayment
{
    public class ExpireSubscriptionPaymentCommand : CommandBase
    {
        public ExpireSubscriptionPaymentCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; }
    }
}