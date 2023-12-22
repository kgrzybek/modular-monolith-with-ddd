using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    internal static class DomainEventTypeMappings
    {
        internal static IDictionary<string, Type> Dictionary { get; }

        static DomainEventTypeMappings()
        {
            Dictionary = new Dictionary<string, Type>();
            Dictionary.Add("SubscriptionPaymentCreated", typeof(SubscriptionPaymentCreatedDomainEvent));
            Dictionary.Add("SubscriptionPaymentPaid", typeof(SubscriptionPaymentPaidDomainEvent));
            Dictionary.Add("SubscriptionPaymentExpired", typeof(SubscriptionPaymentExpiredDomainEvent));

            Dictionary.Add("SubscriptionRenewalPaymentCreated", typeof(SubscriptionRenewalPaymentCreatedDomainEvent));
            Dictionary.Add("SubscriptionRenewalPaymentPaid", typeof(SubscriptionRenewalPaymentPaidDomainEvent));

            Dictionary.Add("SubscriptionCreated", typeof(SubscriptionCreatedDomainEvent));
            Dictionary.Add("SubscriptionRenewed", typeof(SubscriptionRenewedDomainEvent));
            Dictionary.Add("SubscriptionExpired", typeof(SubscriptionExpiredDomainEvent));

            Dictionary.Add("PayerCreated", typeof(PayerCreatedDomainEvent));

            Dictionary.Add("PriceListItemCreated", typeof(PriceListItemCreatedDomainEvent));
            Dictionary.Add("PriceListItemActivated", typeof(PriceListItemActivatedDomainEvent));
            Dictionary.Add("PriceListItemDeactivated", typeof(PriceListItemDeactivatedDomainEvent));
            Dictionary.Add("PriceListItemAttributesChanged", typeof(PriceListItemAttributesChangedDomainEvent));

            Dictionary.Add("MeetingFeeCanceled", typeof(MeetingFeeCanceledDomainEvent));
            Dictionary.Add("MeetingFeeCreated", typeof(MeetingFeeCreatedDomainEvent));
            Dictionary.Add("MeetingFeeExpired", typeof(MeetingFeeExpiredDomainEvent));
            Dictionary.Add("MeetingFeePaid", typeof(MeetingFeePaidDomainEvent));

            Dictionary.Add("MeetingFeePaymentCreated", typeof(MeetingFeePaymentCreatedDomainEvent));
            Dictionary.Add("MeetingFeePaymentExpired", typeof(MeetingFeePaymentExpiredDomainEvent));
            Dictionary.Add("MeetingFeePaymentPaid", typeof(MeetingFeePaymentPaidDomainEvent));
        }
    }
}