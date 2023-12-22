using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.Subscriptions
{
    [TestFixture]
    public class SubscriptionTests : TestBase
    {
        [Test]
        public void CreateSubscription_IsSuccessful()
        {
            // Arrange
            var subscriptionPaymentSnapshot = new SubscriptionPaymentSnapshot(
                new SubscriptionPaymentId(Guid.NewGuid()),
                new PayerId(Guid.NewGuid()),
                SubscriptionPeriod.Month,
                "PL");

            // Act
            var subscription = Subscription.Create(subscriptionPaymentSnapshot);

            // Assert
            AssertPublishedDomainEvent<SubscriptionCreatedDomainEvent>(subscription);
        }

        [Test]
        public void RenewSubscription_IsSuccessful()
        {
            // Arrange
            var subscriptionPaymentSnapshot = new SubscriptionPaymentSnapshot(
                new SubscriptionPaymentId(Guid.NewGuid()),
                new PayerId(Guid.NewGuid()),
                SubscriptionPeriod.Month,
                "PL");

            var subscription = Subscription.Create(subscriptionPaymentSnapshot);

            var subscriptionRenewalPaymentSnapshot = new SubscriptionRenewalPaymentSnapshot(
                new SubscriptionRenewalPaymentId(Guid.NewGuid()),
                new PayerId(Guid.NewGuid()),
                SubscriptionPeriod.Month,
                "PL");

            // Act
            subscription.Renew(subscriptionRenewalPaymentSnapshot);

            // Assert
            AssertPublishedDomainEvent<SubscriptionRenewedDomainEvent>(subscription);
        }

        [Test]
        public void ExpireSubscription_IsSuccessful()
        {
            // Arrange
            var referenceDate = DateTime.UtcNow;

            SystemClock.Set(referenceDate);

            var subscriptionPaymentSnapshot = new SubscriptionPaymentSnapshot(
                new SubscriptionPaymentId(Guid.NewGuid()),
                new PayerId(Guid.NewGuid()),
                SubscriptionPeriod.Month,
                "PL");

            var subscription = Subscription.Create(subscriptionPaymentSnapshot);

            SystemClock.Set(referenceDate.AddMonths(1).AddMilliseconds(1));

            // Act
            subscription.Expire();

            // Assert
            AssertPublishedDomainEvent<SubscriptionExpiredDomainEvent>(subscription);
        }
    }
}