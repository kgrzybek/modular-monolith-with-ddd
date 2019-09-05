using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments.Events
{
    public class MeetingPayedDomainEvent : DomainEventBase
    {
        public MeetingPayedDomainEvent(PayerId payerId, MeetingId meetingId, DateTime paymentDate)
        {
            PayerId = payerId;
            MeetingId = meetingId;
            PaymentDate = paymentDate;
        }

        public PayerId PayerId { get; }

        public MeetingId MeetingId { get; }

        public DateTime PaymentDate { get; }
    }
}