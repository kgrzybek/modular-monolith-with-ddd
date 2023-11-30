using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid
{
    public class MeetingFeePaymentPaidNotification : DomainNotificationBase<MeetingFeePaymentPaidDomainEvent>
    {
        public MeetingFeePaymentPaidNotification(MeetingFeePaymentPaidDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}