using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees
{
    public class MeetingFeeId : AggregateId<MeetingFee>
    {
        public MeetingFeeId(Guid value)
            : base(value)
        {
        }
    }
}