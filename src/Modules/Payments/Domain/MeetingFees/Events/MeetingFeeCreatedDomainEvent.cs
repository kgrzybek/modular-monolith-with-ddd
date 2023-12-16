using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events
{
    public class MeetingFeeCreatedDomainEvent : DomainEventBase
    {
        public MeetingFeeCreatedDomainEvent(
            Guid meetingFeeId,
            Guid payerId,
            Guid meetingId,
            decimal feeValue,
            string feeCurrency,
            string status)
        {
            PayerId = payerId;
            MeetingId = meetingId;
            FeeValue = feeValue;
            FeeCurrency = feeCurrency;
            Status = status;
            MeetingFeeId = meetingFeeId;
        }

        public Guid MeetingFeeId { get; }

        public Guid PayerId { get; }

        public Guid MeetingId { get; }

        public decimal FeeValue { get; }

        public string FeeCurrency { get; }

        public string Status { get; }
    }
}