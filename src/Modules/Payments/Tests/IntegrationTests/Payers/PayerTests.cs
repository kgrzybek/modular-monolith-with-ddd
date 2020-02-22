using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer;
using CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.Payers
{
    [TestFixture]
    public class PayerTests : TestBase
    {
        [Test]
        public async Task CreatePayer_Test()
        {
            var payerId = await PaymentsModule.ExecuteCommandAsync(
                new CreatePayerCommand(Guid.NewGuid(),
                PayerSampleData.Id, PayerSampleData.Login, PayerSampleData.Email, PayerSampleData.FirstName,
                PayerSampleData.LastName, PayerSampleData.Name));

            var payer = await PaymentsModule.ExecuteQueryAsync(new GetPayerQuery(payerId));

            Assert.That(payer.Id, Is.EqualTo(PayerSampleData.Id));
            Assert.That(payer.Login, Is.EqualTo(PayerSampleData.Login));
            Assert.That(payer.Name, Is.EqualTo(PayerSampleData.Name));
            Assert.That(payer.FirstName, Is.EqualTo(PayerSampleData.FirstName));
            Assert.That(payer.LastName, Is.EqualTo(PayerSampleData.LastName));
            Assert.That(payer.Email, Is.EqualTo(PayerSampleData.Email));
        }
    }
}