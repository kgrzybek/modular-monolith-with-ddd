using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events
{
    public class MeetingFeePaymentCreatedDomainEvent : DomainEventBase
    {
        public MeetingFeePaymentCreatedDomainEvent(
            Guid meetingFeePaymentId,
            Guid meetingFeeId,
            string status)
        {
            MeetingFeeId = meetingFeeId;
            Status = status;
            MeetingFeePaymentId = meetingFeePaymentId;
        }

        public Guid MeetingFeeId { get; }

        public Guid MeetingFeePaymentId { get; }

        public string Status { get; }
    }
}