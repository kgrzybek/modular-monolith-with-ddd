using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Subscriptions
{
    [TestFixture]
    public class SubscriptionBuyTests : TestBase
    {
        [Test]
        public async Task BuySubscription_Test()
        {
            var subscriptionId = await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(Guid.NewGuid(),
                "Month",
                "PL"));

            await PaymentsModule.ExecuteCommandAsync(
                new RenewSubscriptionCommand(subscriptionId,
                    "Month"));

            await PaymentsModule.ExecuteCommandAsync(
                new ExpireSubscriptionCommand(subscriptionId));
        }
    }
}