namespace CompanyName.MyMeetings.API.Modules.Payments.PriceListItems
{
    public class GetPriceListItemRequest
    {
        public string CountryCode { get; set; }

        public string CategoryCode { get; set; }

        public string PeriodTypeCode { get; set; }
    }
}