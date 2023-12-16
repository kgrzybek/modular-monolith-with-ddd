using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events
{
    public class MeetingFeeCanceledDomainEvent : DomainEventBase
    {
        public MeetingFeeCanceledDomainEvent(Guid meetingFeeId, string status)
        {
            MeetingFeeId = meetingFeeId;
            Status = status;
        }

        public Guid MeetingFeeId { get; }

        public string Status { get; }
    }
}