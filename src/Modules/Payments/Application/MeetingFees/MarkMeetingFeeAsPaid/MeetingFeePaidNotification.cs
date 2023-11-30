using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    public class MeetingFeePaidNotification : DomainNotificationBase<MeetingFeePaidDomainEvent>
    {
        public MeetingFeePaidNotification(MeetingFeePaidDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}