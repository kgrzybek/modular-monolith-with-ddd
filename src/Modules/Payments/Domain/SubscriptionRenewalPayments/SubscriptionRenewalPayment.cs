using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments
{
    public class SubscriptionRenewalPayment : AggregateRoot
    {
        private PayerId _payerId;

        private SubscriptionId _subscriptionId;

        private SubscriptionPeriod _subscriptionPeriod;

        private string _countryCode;

        private SubscriptionRenewalPaymentStatus _subscriptionRenewalPaymentStatus;

        private MoneyValue _value;

        public static SubscriptionRenewalPayment Buy(
            PayerId payerId,
            SubscriptionId subscriptionId,
            SubscriptionPeriod period,
            string countryCode,
            MoneyValue priceOffer,
            PriceList priceList)
        {
            var priceInPriceList = priceList.GetPrice(countryCode, period, PriceListItemCategory.Renewal);
            CheckRule(new PriceOfferMustMatchPriceInPriceListRule(priceOffer, priceInPriceList));

            var subscriptionRenewalPayment = new SubscriptionRenewalPayment();

            var subscriptionRenewalPaymentCreated = new SubscriptionRenewalPaymentCreatedDomainEvent(
                Guid.NewGuid(),
                payerId.Value,
                subscriptionId.Value,
                period.Code,
                countryCode,
                SubscriptionRenewalPaymentStatus.WaitingForPayment.Code,
                priceOffer.Value,
                priceOffer.Currency);

            subscriptionRenewalPayment.Apply(subscriptionRenewalPaymentCreated);
            subscriptionRenewalPayment.AddDomainEvent(subscriptionRenewalPaymentCreated);

            return subscriptionRenewalPayment;
        }

        public SubscriptionRenewalPaymentSnapshot GetSnapshot()
        {
            return new SubscriptionRenewalPaymentSnapshot(new SubscriptionRenewalPaymentId(this.Id), _payerId, _subscriptionPeriod, _countryCode);
        }

        public void MarkRenewalAsPaid()
        {
            SubscriptionRenewalPaymentPaidDomainEvent @event =
                new SubscriptionRenewalPaymentPaidDomainEvent(
                    this.Id,
                    this._subscriptionId.Value,
                    SubscriptionRenewalPaymentStatus.Paid.Code);

            this.Apply(@event);
            this.AddDomainEvent(@event);
        }

        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        private void When(SubscriptionRenewalPaymentCreatedDomainEvent @event)
        {
            this.Id = @event.SubscriptionRenewalPaymentId;
            _payerId = new PayerId(@event.PayerId);
            _subscriptionId = new SubscriptionId(@event.SubscriptionId);
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _countryCode = @event.CountryCode;
            _subscriptionRenewalPaymentStatus = SubscriptionRenewalPaymentStatus.Of(@event.Status);
            _value = MoneyValue.Of(@event.Value, @event.Currency);
        }

        private void When(SubscriptionRenewalPaymentPaidDomainEvent @event)
        {
            _subscriptionRenewalPaymentStatus = SubscriptionRenewalPaymentStatus.Of(@event.Status);
        }
    }
}