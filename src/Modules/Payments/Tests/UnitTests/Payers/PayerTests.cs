using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.Payers
{
    [TestFixture]
    public class PayerTests : TestBase
    {
        [Test]
        public void CreatePayer_IsSuccessful()
        {
            var payerId = Guid.NewGuid();
            var payer = Payer.Create(
                payerId,
                "payerLogin",
                "payerEmail@mail.com",
                "John",
                "Doe",
                "John Doe");

            var payerCreated = AssertPublishedDomainEvent<PayerCreatedDomainEvent>(payer);

            Assert.That(payerCreated.PayerId, Is.EqualTo(payerId));
        }
    }
}