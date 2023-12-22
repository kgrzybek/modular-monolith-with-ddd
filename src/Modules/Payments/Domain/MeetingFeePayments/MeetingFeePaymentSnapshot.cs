namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments
{
    public class MeetingFeePaymentSnapshot
    {
        public MeetingFeePaymentSnapshot(Guid meetingFeePaymentId, Guid meetingFeeId)
        {
            MeetingFeePaymentId = meetingFeePaymentId;
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeePaymentId { get; }

        public Guid MeetingFeeId { get; }
    }
}