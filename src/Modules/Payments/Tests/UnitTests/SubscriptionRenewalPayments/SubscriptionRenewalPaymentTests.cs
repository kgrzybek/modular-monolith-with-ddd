using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SubscriptionRenewalPayments
{
    [TestFixture]
    public class SubscriptionRenewalPaymentTests : SubscriptionRenewalPaymentTestsBase
    {
        [Test]
        public void BuySubscriptionRenewal_IsSuccessful()
        {
            // Arrange
            var subscriptionRenewalPaymentTestData = CreateSubscriptionRenewalPaymentTestData();

            // Act
            var subscriptionRenewalPayment = SubscriptionRenewalPayment.Buy(
                subscriptionRenewalPaymentTestData.PayerId,
                subscriptionRenewalPaymentTestData.SubscriptionId, 
                SubscriptionPeriod.Month,
                "PL",
                MoneyValue.Of(60, "PLN"),
                subscriptionRenewalPaymentTestData.PriceList
            );
            
            // Assert
            AssertPublishedDomainEvent<SubscriptionRenewalPaymentCreatedDomainEvent>(subscriptionRenewalPayment);
        }

        [Test]
        public void PaySubscriptionRenewal_IsSuccessful()
        {
            // Arrange
            var subscriptionRenewalPaymentTestData = CreateSubscriptionRenewalPaymentTestData();

            var subscriptionRenewalPayment = SubscriptionRenewalPayment.Buy(
                subscriptionRenewalPaymentTestData.PayerId,
                subscriptionRenewalPaymentTestData.SubscriptionId, 
                SubscriptionPeriod.Month,
                "PL",
                MoneyValue.Of(60, "PLN"),
                subscriptionRenewalPaymentTestData.PriceList
            );
            
            // Act
            subscriptionRenewalPayment.MarkRenewalAsPaid();
            
            // Assert
            AssertPublishedDomainEvent<SubscriptionRenewalPaymentPaidDomainEvent>(subscriptionRenewalPayment);
        }
    }
}