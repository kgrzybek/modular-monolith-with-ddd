using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
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

        public void Renew(
            SubscriptionRenewalPaymentSnapshot subscriptionRenewalPayment)
        {
            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForRenewal(
                _expirationDate, subscriptionRenewalPayment.SubscriptionPeriod);

            SubscriptionRenewedDomainEvent subscriptionRenewedDomainEvent = new SubscriptionRenewedDomainEvent(
                this.Id,
                expirationDate,
                subscriptionRenewalPayment.PayerId.Value,
                subscriptionRenewalPayment.SubscriptionPeriod.Code,
                SubscriptionStatus.Active.Code);

            this.Apply(subscriptionRenewedDomainEvent);
            this.AddDomainEvent(subscriptionRenewedDomainEvent);
        }

        public void Expire()
        {
            if (_expirationDate < SystemClock.Now)
            {
                SubscriptionExpiredDomainEvent subscriptionExpiredDomainEvent =
                    new SubscriptionExpiredDomainEvent(this.Id, SubscriptionStatus.Expired.Code);

                this.When(subscriptionExpiredDomainEvent);
                this.AddDomainEvent(subscriptionExpiredDomainEvent);
            }
        }

        public static Subscription Create(
            SubscriptionPaymentSnapshot subscriptionPayment)
        {
            var subscription = new Subscription();

            var expirationDate = SubscriptionDateExpirationCalculator.CalculateForNew(subscriptionPayment.SubscriptionPeriod);

            var subscriptionCreatedDomainEvent = new SubscriptionCreatedDomainEvent(
                subscriptionPayment.Id.Value,
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

        protected sealed override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
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
    }
}