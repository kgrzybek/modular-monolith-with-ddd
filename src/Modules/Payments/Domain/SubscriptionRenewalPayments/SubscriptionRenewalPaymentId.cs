using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments
{
    public class SubscriptionRenewalPaymentId : AggregateId<SubscriptionRenewalPayment>
    {
        public SubscriptionRenewalPaymentId(Guid value)
            : base(value)
        {
        }
    }
}