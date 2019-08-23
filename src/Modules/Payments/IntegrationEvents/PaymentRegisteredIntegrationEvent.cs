using System;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationEvents
{
    public class PaymentRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid MeetingGroupPaymentRegisterId { get; }

        public DateTime DateTo { get; }

        public PaymentRegisteredIntegrationEvent(
            Guid id, 
            DateTime occurredOn, 
            Guid meetingGroupPaymentRegisterId, 
            DateTime dateTo) : base(id, occurredOn)
        {
            MeetingGroupPaymentRegisterId = meetingGroupPaymentRegisterId;
            DateTo = dateTo;
        }
    }
}