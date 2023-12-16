using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events
{
    public class MeetingFeePaymentPaidDomainEvent : DomainEventBase
    {
        public MeetingFeePaymentPaidDomainEvent(Guid meetingFeePaymentId, string status)
        {
            MeetingFeePaymentId = meetingFeePaymentId;
            Status = status;
        }

        public Guid MeetingFeePaymentId { get; }

        public string Status { get; }
    }
}