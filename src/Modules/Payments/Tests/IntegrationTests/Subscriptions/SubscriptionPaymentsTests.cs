using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.PriceList;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Subscriptions
{
    [NonParallelizable]
    [TestFixture]
    public class SubscriptionPaymentsTests : TestBase
    {
        [Test]
        public async Task SubscriptionPayment_Expiration_Test()
        {
            SystemClock.Set(new DateTime(2020, 6, 15, 10, 0, 0));

            await PriceListHelper.AddPriceListItems(PaymentsModule);

            await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(
                "Month",
                "PL",
                60,
                "PLN"));

            var subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(
                    PaymentsModule,
                    ExecutionContext.UserId,
                    x => true),
                10000);

            Assert.That(subscriptionPayments[0].Status, Is.EqualTo(SubscriptionPaymentStatus.WaitingForPayment.Code));

            SystemClock.Set(new DateTime(2020, 6, 15, 10, 21, 0));

            await PaymentsModule.ExecuteCommandAsync(
                new ExpireSubscriptionPaymentsCommand());

            await GetEventually(
                new GetSubscriptionPaymentsProbe(
                    PaymentsModule,
                    ExecutionContext.UserId,
                    x => x.Any(y => y.Status == SubscriptionPaymentStatus.Expired.Code)),
                10000);
        }

        private class GetSubscriptionPaymentsProbe : IProbe<List<SubscriptionPaymentDto>>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _payerId;

            private readonly Func<List<SubscriptionPaymentDto>, bool> _condition;

            public GetSubscriptionPaymentsProbe(
                IPaymentsModule paymentsModule,
                Guid payerId,
                Func<List<SubscriptionPaymentDto>, bool> condition)
            {
                _paymentsModule = paymentsModule;

                _payerId = payerId;
                _condition = condition;
            }

            public bool IsSatisfied(List<SubscriptionPaymentDto> sample)
            {
                return sample != null && _condition(sample);
            }

            public async Task<List<SubscriptionPaymentDto>> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetSubscriptionPaymentsQuery(_payerId));
            }

            public string DescribeFailureTo()
            {
                return $"Cannot get subscription payments for PayerId: {_payerId}";
            }
        }
    }
}