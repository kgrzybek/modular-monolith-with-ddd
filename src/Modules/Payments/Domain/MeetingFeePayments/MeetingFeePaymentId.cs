using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments
{
    public class MeetingFeePaymentId : AggregateId<MeetingFeePayment>
    {
        public MeetingFeePaymentId(Guid value)
            : base(value)
        {
        }
    }
}