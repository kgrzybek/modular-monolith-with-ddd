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

        public static Subscription Buy(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode)
        {
            var subscription = new Subscription();

            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(period);

            var subscriptionPurchasedDomainEvent = new SubscriptionPurchasedDomainEvent(
                Guid.NewGuid(),
                payerId.Value,
                period.Code,
                countryCode,
                expirationDate);

            subscription.Apply(subscriptionPurchasedDomainEvent);
            subscription.AddDomainEvent(subscriptionPurchasedDomainEvent);

            return subscription;
        }

        public void Renew(SubscriptionPeriod period)
        {
            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(_expirationDate, period);
            SubscriptionRenewedDomainEvent subscriptionRenewedDomainEvent = new SubscriptionRenewedDomainEvent(this.Id,
                expirationDate, period.Code);

            this.Apply(subscriptionRenewedDomainEvent);
            this.AddDomainEvent(subscriptionRenewedDomainEvent);
        }

        public void Expire()
        {
            SubscriptionExpiredDomainEvent subscriptionExpiredDomainEvent =
                new SubscriptionExpiredDomainEvent(this.Id);

            this.When(subscriptionExpiredDomainEvent);
            this.AddDomainEvent(subscriptionExpiredDomainEvent);
        }

        private void When(SubscriptionPurchasedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _payerId = new PayerId(@event.PayerId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
            _status = SubscriptionStatus.Active;
            _expirationDate = @event.ExpirationDate;
        }

        private void When(SubscriptionRenewedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _status = SubscriptionStatus.Active;
            _expirationDate = @event.ExpirationDate;
        }

        private void When(SubscriptionExpiredDomainEvent @event)
        {
            _status = SubscriptionStatus.Expired;
        }

        protected sealed override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }
    }
}