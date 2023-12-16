using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments
{
    public class SubscriptionPaymentId : AggregateId<SubscriptionPayment>
    {
        public SubscriptionPaymentId(Guid value)
            : base(value)
        {
        }
    }
}