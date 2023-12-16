using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Subscriptions
{
    internal class GetSubscriptionPaymentsProbe : IProbe<List<SubscriptionPaymentDto>>
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