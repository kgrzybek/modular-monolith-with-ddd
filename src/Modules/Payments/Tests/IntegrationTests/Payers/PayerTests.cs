using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Payers
{
    [NonParallelizable]
    [TestFixture]
    public class PayerTests : TestBase
    {
        [Test]
        public async Task CreatePayer_Test()
        {
            var payerId = await PaymentsModule.ExecuteCommandAsync(
                new CreatePayerCommand(
                    Guid.NewGuid(),
                    PayerSampleData.Id,
                    PayerSampleData.Login,
                    PayerSampleData.Email,
                    PayerSampleData.FirstName,
                    PayerSampleData.LastName,
                    PayerSampleData.Name));

            var payer = await GetEventually(
                new GetPayerProbe(PaymentsModule, payerId),
                5000);

            Assert.That(payer.Id, Is.EqualTo(PayerSampleData.Id));
            Assert.That(payer.Login, Is.EqualTo(PayerSampleData.Login));
            Assert.That(payer.Name, Is.EqualTo(PayerSampleData.Name));
            Assert.That(payer.FirstName, Is.EqualTo(PayerSampleData.FirstName));
            Assert.That(payer.LastName, Is.EqualTo(PayerSampleData.LastName));
            Assert.That(payer.Email, Is.EqualTo(PayerSampleData.Email));
        }

        private class GetPayerProbe : IProbe<PayerDto>
        {
            private readonly IPaymentsModule _paymentsModule;

            private readonly Guid _payerId;

            public GetPayerProbe(
                IPaymentsModule paymentsModule,
                Guid payerId)
            {
                _paymentsModule = paymentsModule;

                _payerId = payerId;
            }

            public bool IsSatisfied(PayerDto sample)
            {
                return sample != null;
            }

            public async Task<PayerDto> GetSampleAsync()
            {
                return await _paymentsModule.ExecuteQueryAsync(new GetPayerQuery(_payerId));
            }

            public string DescribeFailureTo()
            {
                return $"Cannot get payer for PayerId: {_payerId}";
            }
        }
    }
}