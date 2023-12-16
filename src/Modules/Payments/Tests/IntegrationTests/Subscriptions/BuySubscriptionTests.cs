using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetPayerSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.PriceList;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Subscriptions
{
    [TestFixture]
    public class BuySubscriptionTests : TestBase
    {
        [Test]
        public async Task BuySubscription_Test()
        {
            await PriceListHelper.AddPriceListItems(PaymentsModule);

            var subscriptionPaymentId = await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(
                    SubscriptionPeriod.Month.Code,
                    "PL",
                    60,
                    "PLN"));

            var subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(
                    PaymentsModule,
                    ExecutionContext.UserId,
                    x => true),
                10000);

            var subscriptionPayment = subscriptionPayments.Single(x => x.PaymentId == subscriptionPaymentId);

            Assert.That(subscriptionPayment.Status, Is.EqualTo(SubscriptionPaymentStatus.WaitingForPayment.Code));

            await PaymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionPaymentAsPaidCommand(subscriptionPaymentId));

            var subscription = await GetEventually(
                new GetPayerSubscriptionProbe(
                    PaymentsModule,
                    ExecutionContext.UserId),
                10000);

            Assert.That(subscription.Period, Is.EqualTo(SubscriptionPeriod.Month.Code));
            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Active.Code));
        }

        private class GetPayerSubscriptionProbe : IProbe<SubscriptionDetailsDto>
        {
            private readonly IPaymentsModule _paymentsModule;

            public GetPayerSubscriptionProbe(
                IPaymentsModule paymentsModule,
                Guid payerId)
            {
                _paymentsModule = paymentsModule;
            }

            public bool IsSatisfied(SubscriptionDetailsDto sample)
            {
                return sample != null;
            }

            public async Task<SubscriptionDetailsDto> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetAuthenticatedPayerSubscriptionQuery());
            }

            public string DescribeFailureTo() => "Subscription read model is not in expected state";
        }
    }
}