using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.RenewSubscription;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Subscriptions
{
    [TestFixture]
    public class SubscriptionLifecycleTests : TestBase
    {
        [Test]
        public async Task Subscription_Buy_ThenRenew_ThenExpire_Test()
        {
            SystemClock.Set(new DateTime(2020, 6, 15));

            var subscriptionPaymentId = await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionCommand(
                "Month",
                "PL",
                60,
                "PLN"));

            var subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => true), 
                10000);

            Assert.That(subscriptionPayments[0].Status, Is.EqualTo(SubscriptionPaymentStatus.WaitingForPayment.Code));

            await PaymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionPaymentAsPaidCommand(subscriptionPaymentId));

            subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => true),
                10000);

            Assert.That(subscriptionPayments[0].Status, Is.EqualTo(SubscriptionPaymentStatus.Paid.Code));

            subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => x.Any(y => y.SubscriptionId.HasValue)),
                10000);

            var subscriptionId = subscriptionPayments[0].SubscriptionId.Value;

            //AssertEventually(
            //    new GetSubscriptionDetailsProbe(
            //        PaymentsModule, 
            //        subscriptionId,
            //        x => x.SubscriptionId == subscriptionId &&
            //             x.Status == SubscriptionStatus.Active.Code && 
            //             x.Period == SubscriptionPeriod.Month.Code && 
            //            x.ExpirationDate == new DateTime(2020, 7, 15)), 5000);

            //await PaymentsModule.ExecuteCommandAsync(
            //    new RenewSubscriptionCommand(subscriptionId,
            //        "Month"));

            //AssertEventually(
            //    new GetSubscriptionDetailsProbe(
            //        PaymentsModule,
            //        subscriptionId,
            //        x => x.SubscriptionId == subscriptionId &&
            //             x.Status == SubscriptionStatus.Active.Code &&
            //             x.Period == SubscriptionPeriod.Month.Code &&
            //             x.ExpirationDate == new DateTime(2020, 8, 15)), 5000);

            //await PaymentsModule.ExecuteCommandAsync(
            //    new ExpireSubscriptionCommand(subscriptionId));

            //AssertEventually(
            //    new GetSubscriptionDetailsProbe(
            //        PaymentsModule,
            //        subscriptionId,
            //        x => x.SubscriptionId == subscriptionId &&
            //             x.Status == SubscriptionStatus.Expired.Code), 5000);
        }

        private class GetSubscriptionPaymentsProbe : IProbe<List<SubscriptionPaymentDto>>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _payerId;

            private readonly Func<List<SubscriptionPaymentDto>, bool> _condition;

            public GetSubscriptionPaymentsProbe(
                IPaymentsModule paymentsModule, 
                Guid payerId, Func<List<SubscriptionPaymentDto>, bool> condition)
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

        private class GetSubscriptionDetailsProbe : IProbe
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _subscriptionId;

            private SubscriptionDetailsDto _subscriptionDetailsDto;

            private readonly Func<SubscriptionDetailsDto, bool> _condition;

            public GetSubscriptionDetailsProbe(
                IPaymentsModule paymentsModule, 
                Guid subscriptionId, 
                Func<SubscriptionDetailsDto, bool> condition)
            {
                _paymentsModule = paymentsModule;
                _subscriptionId = subscriptionId;
                _condition = condition;
            }

            public bool IsSatisfied()
            {
                return _subscriptionDetailsDto != null && _condition(_subscriptionDetailsDto);
            }

            public async Task SampleAsync()
            {
                _subscriptionDetailsDto =
                    await _paymentsModule.ExecuteQueryAsync(new GetSubscriptionDetailsQuery(_subscriptionId));
            }

            public string DescribeFailureTo() => "Subscription read model is not in expected state";
        }
    }
}