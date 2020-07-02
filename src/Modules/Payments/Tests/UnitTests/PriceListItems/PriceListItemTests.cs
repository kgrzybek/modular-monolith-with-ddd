using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.PriceListItems
{
    [TestFixture]
    public class PriceListItemTests : TestBase
    {
        [Test]
        public void CreatePriceListItem_IsSuccessful()
        {
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));
            
            var priceListItemCreated = AssertPublishedDomainEvent<PriceListItemCreatedDomainEvent>(priceListItem);

            Assert.That(priceListItemCreated.PriceListItemId, Is.EqualTo(priceListItem.Id));
        }
    }
}