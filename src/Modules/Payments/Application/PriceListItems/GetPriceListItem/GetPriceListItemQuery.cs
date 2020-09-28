using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItem
{
    public class GetPriceListItemQuery : QueryBase<PriceListItemMoneyValueDto>
    {
        public GetPriceListItemQuery(string countryCode, string categoryCode, string periodTypeCode)
        {
            CountryCode = countryCode;
            CategoryCode = categoryCode;
            PeriodTypeCode = periodTypeCode;
        }

        public string CountryCode { get; }

        public string CategoryCode { get; }

        public string PeriodTypeCode { get; }
    }
}