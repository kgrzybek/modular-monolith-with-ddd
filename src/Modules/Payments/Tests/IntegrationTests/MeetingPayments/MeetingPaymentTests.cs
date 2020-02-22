using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.CreateMeetingPayment;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.GetMeetingPayment;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.MeetingPayments
{
    [TestFixture]
    public class MeetingPaymentTests : TestBase
    {
        [Test]
        public async Task CreateMeetingPayment_Test()
        {
            PayerId payerId = new PayerId(Guid.NewGuid());
            MeetingId meetingId = new MeetingId(Guid.NewGuid());
            decimal value = 100;
            string currency = "EUR";
            await PaymentsModule.ExecuteCommandAsync(new CreateMeetingPaymentCommand(Guid.NewGuid(),
                payerId, meetingId, value, currency));

            var payment = await PaymentsModule.ExecuteQueryAsync(new GetMeetingPaymentQuery(meetingId.Value, payerId.Value));

            Assert.That(payment.PayerId, Is.EqualTo(payerId.Value));
            Assert.That(payment.MeetingId, Is.EqualTo(meetingId.Value));
            Assert.That(payment.FeeValue, Is.EqualTo(value));
            Assert.That(payment.FeeCurrency, Is.EqualTo(currency));
        }
    }
}