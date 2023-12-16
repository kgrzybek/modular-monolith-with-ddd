using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events
{
    public class PriceListItemCreatedDomainEvent : DomainEventBase
    {
        public PriceListItemCreatedDomainEvent(
        Guid priceListItemId,
        string countryCode,
        string subscriptionPeriodCode,
        string categoryCode,
        decimal price,
        string currency,
        bool isActive)
        {
            PriceListItemId = priceListItemId;
            CountryCode = countryCode;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CategoryCode = categoryCode;
            Price = price;
            Currency = currency;
            IsActive = isActive;
        }

        public Guid PriceListItemId { get; }

        public string CountryCode { get; }

        public string SubscriptionPeriodCode { get; }

        public string CategoryCode { get; }

        public decimal Price { get; }

        public string Currency { get; }

        public bool IsActive { get; }
    }
}