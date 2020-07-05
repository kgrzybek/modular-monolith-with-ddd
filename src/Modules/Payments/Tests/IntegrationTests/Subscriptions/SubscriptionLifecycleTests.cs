using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscriptionRenewal;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.ExpireSubscriptions;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionRenewalPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments;
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
            DateTime referenceDate = new DateTime(2020, 6, 15);
            SystemClock.Set(referenceDate);

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

            var subscriptionPayment = subscriptionPayments.Single(x => x.PaymentId == subscriptionPaymentId);

            Assert.That(subscriptionPayment.Status, Is.EqualTo(SubscriptionPaymentStatus.WaitingForPayment.Code));

            await PaymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionPaymentAsPaidCommand(subscriptionPaymentId));

            subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => x.TrueForAll(payment => payment.SubscriptionId != null)),
                10000);
            
            subscriptionPayment = subscriptionPayments.Single(x => x.PaymentId == subscriptionPaymentId);
            
            var subscriptionId = subscriptionPayment.SubscriptionId.GetValueOrDefault();
            
            var subscription = await GetEventually(
                new GetSubscriptionDetailsProbe(
                    PaymentsModule, 
                    subscriptionId,
                    x => x.SubscriptionId == subscriptionId &&
                         x.Status == SubscriptionStatus.Active.Code && 
                         x.Period == SubscriptionPeriod.Month.Code), 5000);

            Assert.That(subscriptionPayments[0].Status, Is.EqualTo(SubscriptionPaymentStatus.Paid.Code));
            Assert.That(subscription.ExpirationDate, Is.EqualTo(referenceDate.AddMonths(1)));

            var subscriptionRenewalPaymentId = await PaymentsModule.ExecuteCommandAsync(
                new BuySubscriptionRenewalCommand(
                    subscriptionId,
                    "HalfYear",
                    "PL",
                    320,
                    "PLN"));

            subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => true), 
                10000);

            var renewalPayment = subscriptionPayments
                .Single(x => x.PaymentId == subscriptionRenewalPaymentId);

            Assert.That(renewalPayment.Status, Is.EqualTo(SubscriptionRenewalPaymentStatus.WaitingForPayment.Code));

            await PaymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionRenewalPaymentAsPaidCommand(subscriptionRenewalPaymentId));

            subscriptionPayments = await GetEventually(
                new GetSubscriptionPaymentsProbe(PaymentsModule, ExecutionContext.UserId,
                    x => true), 
                10000);
            
            renewalPayment = subscriptionPayments
                .Single(x => x.PaymentId == subscriptionRenewalPaymentId);

            subscription = await GetEventually(
                new GetSubscriptionDetailsProbe(
                    PaymentsModule, 
                    subscriptionId,
                    x => x.SubscriptionId == subscriptionId &&
                         x.Period == SubscriptionPeriod.GetName(SubscriptionPeriod.HalfYear.Code) &&
                         x.Status == SubscriptionStatus.Active.Code
                    ), 5000);
            
            Assert.That(renewalPayment.Status, Is.EqualTo(SubscriptionRenewalPaymentStatus.Paid.Code));
            Assert.That(subscription.ExpirationDate, Is.EqualTo(referenceDate.AddMonths(7)));

            SystemClock.Set(referenceDate.AddMonths(7).AddDays(1));

            await PaymentsModule.ExecuteCommandAsync(new ExpireSubscriptionsCommand());

            subscription = await GetEventually(
                new GetSubscriptionDetailsProbe(
                    PaymentsModule,
                    subscriptionId,
                    x => x.SubscriptionId == subscriptionId),
                10000);

            Assert.That(subscription.Status, Is.EqualTo(SubscriptionStatus.Expired));

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

        private class GetSubscriptionDetailsProbe : IProbe<SubscriptionDetailsDto>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _subscriptionId;

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
            
            public bool IsSatisfied(SubscriptionDetailsDto sample)
            {
                return sample != null && _condition(sample);
            }

            public async Task<SubscriptionDetailsDto> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetSubscriptionDetailsQuery(_subscriptionId));
            }

            public string DescribeFailureTo() => "Subscription read model is not in expected state";
        }
    }
}