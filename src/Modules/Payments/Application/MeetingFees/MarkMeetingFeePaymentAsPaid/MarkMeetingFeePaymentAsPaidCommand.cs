using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid
{
    public class MarkMeetingFeePaymentAsPaidCommand : CommandBase
    {
        public MarkMeetingFeePaymentAsPaidCommand(Guid meetingFeePaymentId)
        {
            MeetingFeePaymentId = meetingFeePaymentId;
        }

        public Guid MeetingFeePaymentId { get; }
    }
}