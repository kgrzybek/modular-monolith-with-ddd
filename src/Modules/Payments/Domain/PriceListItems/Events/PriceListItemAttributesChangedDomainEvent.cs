using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events
{
    public class PriceListItemAttributesChangedDomainEvent : DomainEventBase
    {
        public PriceListItemAttributesChangedDomainEvent(Guid priceListItemId, string countryCode, string subscriptionPeriodCode, string categoryCode, decimal price, string currency)
        {
            PriceListItemId = priceListItemId;
            CountryCode = countryCode;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CategoryCode = categoryCode;
            Price = price;
            Currency = currency;
        }

        public Guid PriceListItemId { get; }

        public string CountryCode { get; }

        public string SubscriptionPeriodCode { get; }

        public string CategoryCode { get; }

        public decimal Price { get; }

        public string Currency { get; }
    }
}