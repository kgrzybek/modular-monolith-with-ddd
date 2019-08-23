using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events
{
    public class PaymentRegisteredDomainEvent : DomainEventBase
    {
        public PaymentRegisteredDomainEvent(MeetingGroupPaymentRegisterId meetingGroupPaymentRegisterId, DateTime dateTo)
        {
            MeetingGroupPaymentRegisterId = meetingGroupPaymentRegisterId;
            DateTo = dateTo;
        }

        public MeetingGroupPaymentRegisterId MeetingGroupPaymentRegisterId { get; }

        public DateTime DateTo { get; }
    }
}