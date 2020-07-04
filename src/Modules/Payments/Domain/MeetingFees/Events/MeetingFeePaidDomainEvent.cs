using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events
{
    public class MeetingFeePaidDomainEvent : DomainEventBase
    {
        public MeetingFeePaidDomainEvent(Guid meetingFeeId, string status)
        {
            MeetingFeeId = meetingFeeId;
            Status = status;
        }

        public Guid MeetingFeeId { get; }

        public string Status { get; }
    }
}