using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFeePayment
{
    public class CreateMeetingFeePaymentCommand : CommandBase<Guid>
    {
        public CreateMeetingFeePaymentCommand(Guid meetingFeeId)
        {
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }
    }
}