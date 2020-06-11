using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class Subscription : AggregateRoot
    {
        private PayerId _payerId;

        private SubscriptionPeriod _subscriptionPeriod;

        private SubscriptionStatus _status;

        private string _countryCode;

        private DateTime _expirationDate;

        private Subscription()
        {

        }

        private Subscription(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode,
            DateTime expirationDate)
        {
            var subscriptionPurchasedDomainEvent = new SubscriptionPurchasedDomainEvent(
                Guid.NewGuid(),
                payerId.Value, 
                period.Code, 
                countryCode,
                expirationDate);
            
            this.Apply(subscriptionPurchasedDomainEvent);
            this.AddDomainEvent(subscriptionPurchasedDomainEvent);
        }

        public void Apply(SubscriptionPurchasedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _payerId = new PayerId(@event.PayerId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
            _status = SubscriptionStatus.Active;
            _expirationDate = @event.ExpirationDate;
        }

        public static Subscription Buy(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode)
        {
            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(period);

            return new Subscription(payerId, period, countryCode, expirationDate);
        }

        public void Renew(SubscriptionPeriod period)
        {
            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(_expirationDate, period);
            SubscriptionRenewedDomainEvent subscriptionRenewedDomainEvent = new SubscriptionRenewedDomainEvent(this.Id,
                expirationDate, period.Code);

            this.Apply(subscriptionRenewedDomainEvent);
            this.AddDomainEvent(subscriptionRenewedDomainEvent);
        }

        public void Apply(SubscriptionRenewedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _status = SubscriptionStatus.Active;
            _expirationDate = @event.ExpirationDate;
        }

        public void Expire()
        {
            SubscriptionExpiredDomainEvent subscriptionExpiredDomainEvent = 
                new SubscriptionExpiredDomainEvent(this.Id);

            this.Apply(subscriptionExpiredDomainEvent);
            this.AddDomainEvent(subscriptionExpiredDomainEvent);
        }

        public void Apply(SubscriptionExpiredDomainEvent @event)
        {
            _status = SubscriptionStatus.Expired;
        }

        protected override void Apply(IDomainEvent @event)
        {
            this.Apply((dynamic)@event);
        }
    }
}