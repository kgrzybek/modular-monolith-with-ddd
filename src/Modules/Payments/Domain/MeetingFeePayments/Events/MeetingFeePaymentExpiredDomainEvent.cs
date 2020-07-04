using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events
{
    public class MeetingFeePaymentExpiredDomainEvent : DomainEventBase
    {
        public MeetingFeePaymentExpiredDomainEvent(Guid meetingFeePaymentId, string status)
        {
            MeetingFeePaymentId = meetingFeePaymentId;
            Status = status;
        }

        public Guid MeetingFeePaymentId { get; }

        public string Status { get; }
    }
}