using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems;
using CompanyName.MyMeetings.Modules.Payments.Domain.PriceListItems.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
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
            // Act
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));

            // Assert
            var priceListItemCreated = AssertPublishedDomainEvent<PriceListItemCreatedDomainEvent>(priceListItem);

            Assert.That(priceListItemCreated.PriceListItemId, Is.EqualTo(priceListItem.Id));
        }

        [Test]
        public void ActivatePriceListItem_IsSuccessful()
        {
            // Arrange
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));
            priceListItem.Deactivate();

            // Act
            priceListItem.Activate();

            // Assert
            var priceListItemActivated = AssertPublishedDomainEvent<PriceListItemActivatedDomainEvent>(priceListItem);

            Assert.That(priceListItemActivated.PriceListItemId, Is.EqualTo(priceListItem.Id));
        }

        [Test]
        public void ActivatePriceListItem_WhenItemIsActive_ThenActivationIgnored()
        {
            // Arrange
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));

            // Act
            priceListItem.Activate();

            // Assert
            AssertDomainEventNotPublished<PriceListItemActivatedDomainEvent>(priceListItem);
        }

        [Test]
        public void DeactivatePriceListItem_IsSuccessful()
        {
            // Arrange
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));

            // Act
            priceListItem.Deactivate();

            // Assert
            var priceListItemActivated = AssertPublishedDomainEvent<PriceListItemDeactivatedDomainEvent>(priceListItem);

            Assert.That(priceListItemActivated.PriceListItemId, Is.EqualTo(priceListItem.Id));
        }

        [Test]
        public void DeactivatePriceListItem_WhenItemNotActive_ThenDeactivationIgnored()
        {
            // Arrange
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));
            priceListItem.Deactivate();

            // Act
            priceListItem.Deactivate();

            // Assert
            var priceListItemActivatedEvents = AssertPublishedDomainEvents<PriceListItemDeactivatedDomainEvent>(priceListItem);

            Assert.That(priceListItemActivatedEvents.Count, Is.EqualTo(1));
        }

        [Test]
        public void ChangePriceListItem_IsSuccessful()
        {
            // Arrange
            var priceListItem = PriceListItem.Create(
                "BRA",
                SubscriptionPeriod.Month,
                PriceListItemCategory.New,
                MoneyValue.Of(50, "BRL"));

            // Act
            priceListItem.ChangeAttributes(
                "ARG",
                SubscriptionPeriod.HalfYear,
                PriceListItemCategory.Renewal,
                MoneyValue.Of(25, "ARS"));

            // Assert
            var priceListItemAttributesChangedEvent = AssertPublishedDomainEvent<PriceListItemAttributesChangedDomainEvent>(priceListItem);

            Assert.That(priceListItemAttributesChangedEvent.PriceListItemId, Is.EqualTo(priceListItem.Id));
        }
    }
}