using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.ChangePriceListItemAttributes
{
    public class ChangePriceListItemAttributesCommand : CommandBase
    {
        public ChangePriceListItemAttributesCommand(Guid priceListItemId, string countryCode, string subscriptionPeriodCode, string categoryCode, decimal priceValue, string priceCurrency)
        {
            PriceListItemId = priceListItemId;
            CountryCode = countryCode;
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CategoryCode = categoryCode;
            PriceValue = priceValue;
            PriceCurrency = priceCurrency;
        }

        public Guid PriceListItemId { get; }

        public string CountryCode { get; }

        public string SubscriptionPeriodCode { get; }

        public string CategoryCode { get; }

        public decimal PriceValue { get; }

        public string PriceCurrency { get; }
    }
}