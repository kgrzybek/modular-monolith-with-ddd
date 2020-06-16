using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class Subscription : AggregateRoot
    {
        private SubscriberId _subscriberId;

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

            var subscriptionPurchasedDomainEvent = new SubscriptionCreatedDomainEvent(
                Guid.NewGuid(),
                payerId.Value,
                period.Code,
                countryCode,
                expirationDate,
                SubscriptionStatus.Active.Code);

            subscription.Apply(subscriptionPurchasedDomainEvent);
            subscription.AddDomainEvent(subscriptionPurchasedDomainEvent);

            return subscription;
        }

        public void Renew(SubscriptionPeriod period)
        {
            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(_expirationDate, period);
            SubscriptionRenewedDomainEvent subscriptionRenewedDomainEvent = new SubscriptionRenewedDomainEvent(
                this.Id,
                expirationDate, 
                period.Code,
                SubscriptionStatus.Active.Code
                );

            this.Apply(subscriptionRenewedDomainEvent);
            this.AddDomainEvent(subscriptionRenewedDomainEvent);
        }

        public void Expire()
        {
            SubscriptionExpiredDomainEvent subscriptionExpiredDomainEvent =
                new SubscriptionExpiredDomainEvent(this.Id, SubscriptionStatus.Expired.Code);

            this.When(subscriptionExpiredDomainEvent);
            this.AddDomainEvent(subscriptionExpiredDomainEvent);
        }

        private void When(SubscriptionCreatedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _subscriberId = new SubscriberId(@event.PayerId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
            _status = SubscriptionStatus.Of(@event.Status);
            _expirationDate = @event.ExpirationDate;
        }

        private void When(SubscriptionRenewedDomainEvent @event)
        {
            this.Id = @event.SubscriptionId;
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _status = SubscriptionStatus.Of(@event.Status);
            _expirationDate = @event.ExpirationDate;
        }

        private void When(SubscriptionExpiredDomainEvent @event)
        {
            _status = SubscriptionStatus.Of(@event.Status);
        }

        protected sealed override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        public static Subscription Create(
            SubscriptionPaymentSnapshot subscriptionPayment)
        {
            var subscription = new Subscription();

            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(subscriptionPayment.SubscriptionPeriod);

            var subscriptionCreatedDomainEvent = new SubscriptionCreatedDomainEvent(
                Guid.NewGuid(),
                subscriptionPayment.PayerId.Value,
                subscriptionPayment.SubscriptionPeriod.Code,
                subscriptionPayment.CountryCode,
                expirationDate,
                SubscriptionStatus.Active.Code);

            subscription.Apply(subscriptionCreatedDomainEvent);
            subscription.AddDomainEvent(subscriptionCreatedDomainEvent);

            return subscription;
        }
    }
}