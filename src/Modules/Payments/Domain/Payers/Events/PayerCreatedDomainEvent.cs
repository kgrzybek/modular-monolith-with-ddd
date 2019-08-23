using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events
{
    public class PayerCreatedDomainEvent : DomainEventBase
    {
        public PayerId PayerId { get; }
        
        public PayerCreatedDomainEvent(PayerId payerId)
        {
            PayerId = payerId;
        }
    }
}