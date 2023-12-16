using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupPaymentInfoUpdatedDomainEvent : DomainEventBase
    {
        public MeetingGroupPaymentInfoUpdatedDomainEvent(MeetingGroupId meetingGroupId, DateTime paymentDateTo)
        {
            MeetingGroupId = meetingGroupId;
            PaymentDateTo = paymentDateTo;
        }

        public MeetingGroupId MeetingGroupId { get; }

        public DateTime PaymentDateTo { get; }
    }
}