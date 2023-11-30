using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid
{
    public class SubscriptionRenewalPaymentPaidNotification
        : DomainNotificationBase<SubscriptionRenewalPaymentPaidDomainEvent>
    {
        public SubscriptionRenewalPaymentPaidNotification(SubscriptionRenewalPaymentPaidDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}