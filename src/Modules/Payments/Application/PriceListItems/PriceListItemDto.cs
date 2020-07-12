namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems
{
    public class PriceListItemDto
    {
        public string CountryCode { get; set; }

        public string SubscriptionPeriodCode { get; set; }

        public decimal MoneyValue { get; set; }

        public string MoneyCurrency { get; set; }

        public string CategoryCode { get; set; }
    }
}