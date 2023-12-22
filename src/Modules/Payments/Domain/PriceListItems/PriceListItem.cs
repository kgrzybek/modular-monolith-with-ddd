using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceListItem : AggregateRoot
    {
        private string _countryCode;

        private SubscriptionPeriod _subscriptionPeriod;

        private PriceListItemCategory _category;

        private MoneyValue _price;

        private bool _isActive;

        private PriceListItem()
        {
        }

        public static PriceListItem Create(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category,
            MoneyValue price)
        {
            var priceListItem = new PriceListItem();

            var priceListItemCreatedEvent = new PriceListItemCreatedDomainEvent(
                Guid.NewGuid(),
                countryCode,
                subscriptionPeriod.Code,
                category.Code,
                price.Value,
                price.Currency,
                isActive: true);

            priceListItem.Apply(priceListItemCreatedEvent);
            priceListItem.AddDomainEvent(priceListItemCreatedEvent);

            return priceListItem;
        }

        public void Activate()
        {
            if (!_isActive)
            {
                var priceListItemActivatedEvent = new PriceListItemActivatedDomainEvent(this.Id);

                this.Apply(priceListItemActivatedEvent);
                this.AddDomainEvent(priceListItemActivatedEvent);
            }
        }

        public void Deactivate()
        {
            if (_isActive)
            {
                var priceListItemDeactivatedEvent = new PriceListItemDeactivatedDomainEvent(this.Id);

                this.Apply(priceListItemDeactivatedEvent);
                this.AddDomainEvent(priceListItemDeactivatedEvent);
            }
        }

        public void ChangeAttributes(
            string countryCode,
            SubscriptionPeriod subscriptionPeriod,
            PriceListItemCategory category,
            MoneyValue price)
        {
            var priceListItemChangedDomainEvent = new PriceListItemAttributesChangedDomainEvent(this.Id, countryCode, subscriptionPeriod.Code, category.Code, price.Value, price.Currency);

            this.Apply(priceListItemChangedDomainEvent);
            this.AddDomainEvent(priceListItemChangedDomainEvent);
        }

        protected override void Apply(IDomainEvent @event) => When((dynamic)@event);

        private void When(PriceListItemActivatedDomainEvent @event)
        {
            this._isActive = true;
        }

        private void When(PriceListItemCreatedDomainEvent @event)
        {
            this.Id = @event.PriceListItemId;
            _countryCode = @event.CountryCode;
            _subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            _category = PriceListItemCategory.Of(@event.CategoryCode);
            _price = MoneyValue.Of(@event.Price, @event.Currency);
            _isActive = true;
        }

        private void When(PriceListItemDeactivatedDomainEvent @event)
        {
            this._isActive = false;
        }

        private void When(PriceListItemAttributesChangedDomainEvent @event)
        {
            this._countryCode = @event.CountryCode;
            this._subscriptionPeriod = SubscriptionPeriod.Of(@event.SubscriptionPeriodCode);
            this._category = PriceListItemCategory.Of(@event.CategoryCode);
            this._price = MoneyValue.Of(@event.Price, @event.Currency);
        }
    }
}