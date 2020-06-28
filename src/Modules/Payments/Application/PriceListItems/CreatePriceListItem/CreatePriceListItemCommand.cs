using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.CreatePriceListItem
{
    public class CreatePriceListItemCommand : InternalCommandBase<Guid>
    {
        public string CountryCode { get; }

        public string SubscriptionPeriodCode { get; }
        
        public string CategoryCode { get; }
        
        public decimal PriceValue { get; }

        public string PriceCurrency { get; }

        [JsonConstructor]
        public CreatePriceListItemCommand(Guid id, string subscriptionPeriodCode, string categoryCode, string countryCode, decimal priceValue, string priceCurrency) : base(id)
        {
            SubscriptionPeriodCode = subscriptionPeriodCode;
            CategoryCode = categoryCode;
            CountryCode = countryCode;
            PriceValue = priceValue;
            PriceCurrency = priceCurrency;
        }
    }
}