using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.PricingStrategies;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SubscriptionRenewalPayments
{
    public class SubscriptionRenewalPaymentTestsBase : TestBase
    {
        protected class SubscriptionRenewalPaymentTestData
        {
            public SubscriptionRenewalPaymentTestData(PriceList priceList, PayerId payerId, SubscriptionId subscriptionId)
            {
                PriceList = priceList;
                PayerId = payerId;
                SubscriptionId = subscriptionId;
            }

            internal PriceList PriceList { get; }

            internal PayerId PayerId { get; }

            internal SubscriptionId SubscriptionId { get; }
        }

        protected SubscriptionRenewalPaymentTestData CreateSubscriptionRenewalPaymentTestData()
        {
            var payerId = new PayerId(Guid.NewGuid());
            var subscriptionId = new SubscriptionId(Guid.NewGuid());
            var priceList = CreatePriceList();

            var subscriptionRenewalPaymentTestData = new SubscriptionRenewalPaymentTestData(
                priceList,
                payerId,
                subscriptionId);

            return subscriptionRenewalPaymentTestData;
        }

        private PriceList CreatePriceList()
        {
            var priceListItem = new PriceListItemData(
                "PL",
                SubscriptionPeriod.Month,
                MoneyValue.Of(60, "PLN"),
                PriceListItemCategory.Renewal);

            List<PriceListItemData> priceListItems = [priceListItem];
            var priceList = PriceList.Create(priceListItems, new DirectValueFromPriceListPricingStrategy(priceListItems));

            return priceList;
        }
    }
}