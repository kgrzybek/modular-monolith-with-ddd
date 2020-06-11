using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions;
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
            await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(Guid.NewGuid(),
                "Month",
                "PL"));
        }
    }
}