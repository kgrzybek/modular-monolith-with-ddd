using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid
{
    public class SubscriptionPaymentPaidNotification : DomainNotificationBase<SubscriptionPaymentPaidDomainEvent>
    {
        public SubscriptionPaymentPaidNotification(SubscriptionPaymentPaidDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}