using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem
{
    public class CreatePriceListItemCommand : CommandBase<Guid>
    {
        public string CountryCode { get; }

        public string SubscriptionPeriodCode { get; }

        public string CategoryCode { get; }

        public decimal PriceValue { get; }

        public string PriceCurrency { get; }

        public CreatePriceListItemCommand(string subscriptionPeriodCode, string categoryCode, string countryCode, decimal priceValue, string priceCurrency)
        {
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CategoryCode = categoryCode;
            CountryCode = countryCode;
            PriceValue = priceValue;
            PriceCurrency = priceCurrency;
        }
    }
}