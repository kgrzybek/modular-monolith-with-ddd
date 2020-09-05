using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SubscriptionPayments
{
    [TestFixture]
    public class SubscriptionPaymentTests : SubscriptionPaymentTestsBase
    {
        [Test]
        public void BuySubscription_IsSuccessful()
        {
            // Arrange
            var subscriptionPaymentTestData = CreateSubscriptionPaymentTestData();

            // Act
            var subscriptionPayment = SubscriptionPayment.Buy(
                subscriptionPaymentTestData.PayerId,
                SubscriptionPeriod.Month,
                "PL",
                MoneyValue.Of(60, "PLN"),
                subscriptionPaymentTestData.PriceList);

            // Assert
            AssertPublishedDomainEvent<SubscriptionPaymentCreatedDomainEvent>(subscriptionPayment);
        }

        [Test]
        public void BuySubscriptionRenewal_WhenPriceDoesNotExist_IsNotPossible()
        {
            // Arrange
            var subscriptionPaymentTestData = CreateSubscriptionPaymentTestData();

            // Act & Assert
            AssertBrokenRule<PriceOfferMustMatchPriceInPriceListRule>(() =>
            {
                SubscriptionPayment.Buy(
                    subscriptionPaymentTestData.PayerId,
                    SubscriptionPeriod.Month,
                    "PL",
                    MoneyValue.Of(50, "PLN"),
                    subscriptionPaymentTestData.PriceList);
            });
        }

        [Test]
        public void PaySubscription_IsSuccessful()
        {
            // Arrange
            var subscriptionPaymentTestData = CreateSubscriptionPaymentTestData();

            var subscriptionPayment = SubscriptionPayment.Buy(
                subscriptionPaymentTestData.PayerId,
                SubscriptionPeriod.Month,
                "PL",
                MoneyValue.Of(60, "PLN"),
                subscriptionPaymentTestData.PriceList);

            // Act
            subscriptionPayment.MarkAsPaid();

            // Assert
            AssertPublishedDomainEvent<SubscriptionPaymentPaidDomainEvent>(subscriptionPayment);
        }

        [Test]
        public void ExpireSubscription_IsSuccessful()
        {
            // Arrange
            var subscriptionPaymentTestData = CreateSubscriptionPaymentTestData();

            var subscriptionPayment = SubscriptionPayment.Buy(
                subscriptionPaymentTestData.PayerId,
                SubscriptionPeriod.Month,
                "PL",
                MoneyValue.Of(60, "PLN"),
                subscriptionPaymentTestData.PriceList);

            // Act
            subscriptionPayment.Expire();

            // Assert
            AssertPublishedDomainEvent<SubscriptionPaymentExpiredDomainEvent>(subscriptionPayment);
        }
    }
}