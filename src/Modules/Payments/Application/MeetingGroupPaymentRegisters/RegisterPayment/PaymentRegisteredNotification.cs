using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment
{
    public class PaymentRegisteredNotification : DomainNotificationBase<PaymentRegisteredDomainEvent>
    {
        [JsonConstructor]
        public PaymentRegisteredNotification(PaymentRegisteredDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
        }

    }
}