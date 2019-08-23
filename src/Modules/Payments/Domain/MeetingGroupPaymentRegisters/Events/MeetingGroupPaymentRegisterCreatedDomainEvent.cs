using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events
{
    public class MeetingGroupPaymentRegisterCreatedDomainEvent : DomainEventBase
    {
        public MeetingGroupPaymentRegisterCreatedDomainEvent(MeetingGroupPaymentRegisterId meetingGroupPaymentRegisterId)
        {
            MeetingGroupPaymentRegisterId = meetingGroupPaymentRegisterId;
        }

        public MeetingGroupPaymentRegisterId MeetingGroupPaymentRegisterId { get; }
    }
}