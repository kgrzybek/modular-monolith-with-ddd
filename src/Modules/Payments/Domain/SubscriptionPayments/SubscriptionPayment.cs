using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments
{
    public class SubscriptionPayment : AggregateRoot
    {
        private PayerId _payerId;

        private SubscriptionPeriod _subscriptionPeriod;

        private string _countryCode;

        private SubscriptionPaymentStatus _subscriptionPaymentStatus;

        private MoneyValue _value;

        public static SubscriptionPayment Buy(
            PayerId payerId,
            SubscriptionPeriod period,
            string countryCode,
            MoneyValue priceOffer,
            PriceList priceList)
        {
            var priceInPriceList = priceList.GetPrice(countryCode, period, PriceListItemCategory.New);
            CheckRule(new PriceOfferMustMatchPriceInPriceListRule(priceOffer, priceInPriceList));

            var subscriptionPayment = new SubscriptionPayment();

            var subscriptionPaymentCreated = new SubscriptionPaymentCreatedDomainEvent(
                Guid.NewGuid(),
                payerId.Value,
                period.Code,
                countryCode,
                SubscriptionPaymentStatus.WaitingForPayment.Code,
                priceOffer.Value,
                priceOffer.Currency);

            subscriptionPayment.Apply(subscriptionPaymentCreated);
            subscriptionPayment.AddDomainEvent(subscriptionPaymentCreated);

            return subscriptionPayment;
        }

        public SubscriptionPaymentSnapshot GetSnapshot()
        {
            return new SubscriptionPaymentSnapshot(new SubscriptionPaymentId(this.Id), _payerId, _subscriptionPeriod, _countryCode);
        }

        public void MarkAsPaid()
        {
            SubscriptionPaymentPaidDomainEvent @event =
                new SubscriptionPaymentPaidDomainEvent(
                    this.Id,
                    SubscriptionPaymentStatus.Paid.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        public void Expire()
        {
            SubscriptionPaymentExpiredDomainEvent @event =
                new SubscriptionPaymentExpiredDomainEvent(
                    this.Id,
                    SubscriptionPaymentStatus.Expired.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private void When(SubscriptionPaymentPaidDomainEvent @event)
        {
            _subscriptionPaymentStatus = SubscriptionPaymentStatus.Of(@event.Status);
        }

        private void When(SubscriptionPaymentCreatedDomainEvent @event)
        {
            this.Id = @event.SubscriptionPaymentId;
            _payerId = new PayerId(@event.PayerId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
            _subscriptionPaymentStatus = SubscriptionPaymentStatus.Of(@event.Status);
            _value = MoneyValue.Of(@event.Value, @event.Currency);
        }

        private void When(SubscriptionPaymentExpiredDomainEvent @event)
        {
            _subscriptionPaymentStatus = SubscriptionPaymentStatus.Of(@event.Status);
        }
    }
}