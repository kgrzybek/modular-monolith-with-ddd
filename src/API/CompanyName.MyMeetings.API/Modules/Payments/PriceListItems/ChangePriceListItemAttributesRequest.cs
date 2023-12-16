namespace CompanyName.MyMeetings.API.Modules.Payments.PriceListItems
{
    public class ChangePriceListItemAttributesRequest
    {
        public Guid PriceListItemId { get; set; }

        public string CountryCode { get; set; }

        public string SubscriptionPeriodCode { get; set; }

        public string CategoryCode { get; set; }

        public decimal PriceValue { get; set; }

        public string PriceCurrency { get; set; }
    }
}