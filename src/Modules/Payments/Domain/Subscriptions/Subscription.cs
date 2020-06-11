using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class Subscription : AggregateRoot
    {
        private PayerId _payerId;

        private SubscriptionPeriod _subscriptionPeriod;

        private string _countryCode;

        private Subscription(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode)
        {
            var subscriptionPurchasedDomainEvent = new SubscriptionPurchasedDomainEvent(
                Guid.NewGuid(),
                payerId.Value, 
                period.Code, 
                countryCode);
            
            this.Apply(subscriptionPurchasedDomainEvent);
            this.AddDomainEvent(subscriptionPurchasedDomainEvent);
        }

        public static Subscription Buy(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode)
        {
            return new Subscription(payerId, period, countryCode);
        }

        public void Apply(SubscriptionPurchasedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _payerId = new PayerId(@event.PayerId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
        }
    }
}