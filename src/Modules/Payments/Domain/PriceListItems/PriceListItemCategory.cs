using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems
{
    public class PriceListItemCategory : ValueObject
    {
        public static PriceListItemCategory New => new PriceListItemCategory(nameof(New));

        public static PriceListItemCategory Renewal => new PriceListItemCategory(nameof(Renewal));

        public string Code { get; }

        private PriceListItemCategory(string code)
        {
            Code = code;
        }

        public static PriceListItemCategory Of(string code) => new PriceListItemCategory(code);
    }
}