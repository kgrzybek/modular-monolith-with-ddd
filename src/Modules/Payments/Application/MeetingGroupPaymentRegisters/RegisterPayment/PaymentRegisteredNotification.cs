using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment
{
    public class PaymentRegisteredNotification : DomainNotificationBase<PaymentRegisteredDomainEvent>
    {
        public PaymentRegisteredNotification(PaymentRegisteredDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
            MeetingGroupPaymentRegisterId = domainEvent.MeetingGroupPaymentRegisterId;
            DateTo = domainEvent.DateTo;
        }

        public MeetingGroupPaymentRegisterId MeetingGroupPaymentRegisterId { get; }

        public DateTime DateTo { get; }
    }
}