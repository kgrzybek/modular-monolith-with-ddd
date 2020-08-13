using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Rules
{
    public class PriceOfferMustMatchPriceInPriceListRule : IBusinessRule
    {
        private readonly MoneyValue _priceOffer;

        private readonly MoneyValue _priceInPriceList;

        public PriceOfferMustMatchPriceInPriceListRule(
            MoneyValue priceOffer,
            MoneyValue priceInPriceList)
        {
            _priceOffer = priceOffer;
            _priceInPriceList = priceInPriceList;
        }

        public bool IsBroken() => _priceOffer != _priceInPriceList;

        public string Message => "Price offer must match price in Price List";
    }
}