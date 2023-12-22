using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.BuySubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetPayerSubscription;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.MarkSubscriptionPaymentAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using CompanyName.MyMeetings.SUT.SeedWork;
using CompanyName.MyMeetings.SUT.SeedWork.Probing;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class TestPaymentsManager
    {
        public static async Task BuySubscription(
            IPaymentsModule paymentsModule,
            IExecutionContextAccessor executionContextAccessor)
        {
            var subscriptionPaymentId = await paymentsModule.ExecuteCommandAsync(new BuySubscriptionCommand(
                SubscriptionPeriod.Month.Code,
                "PL",
                60,
                "PLN"));

            await TestBase.GetEventually(
                new GetSubscriptionPaymentsProbe(
                    paymentsModule,
                    executionContextAccessor.UserId,
                    x => true),
                10000);

            await paymentsModule.ExecuteCommandAsync(
                new MarkSubscriptionPaymentAsPaidCommand(subscriptionPaymentId));

            await TestBase.GetEventually(
                new GetPayerSubscriptionProbe(
                    paymentsModule,
                    executionContextAccessor.UserId),
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