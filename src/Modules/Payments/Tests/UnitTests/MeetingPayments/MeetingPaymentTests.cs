using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.MeetingPayments
{
    [TestFixture]
    public class MeetingPaymentTests : TestBase
    {
        [Test]
        public void MeetingPayment_WhenFeeIsGreaterThanZero_IsCreated()
        {
            var payerId = new PayerId(Guid.NewGuid());
            var meetingId = new MeetingId(Guid.NewGuid());
            var fee = MoneyValue.Of(100, "EUR");

            var meetingPayment = MeetingPayment.CreatePaymentForMeeting(payerId, meetingId, fee);

            var meetingCreated = AssertPublishedDomainEvent<MeetingPaymentCreatedDomainEvent>(meetingPayment);
            Assert.That(meetingCreated.PayerId, Is.EqualTo(payerId));
            Assert.That(meetingCreated.MeetingId, Is.EqualTo(meetingId));
            Assert.That(meetingCreated.Fee, Is.EqualTo(fee));
        }

        [Test]
        public void MeetingPayment_WhenFeeIsNotGreaterThanZero_CannotBeCreated()
        {
            var payerId = new PayerId(Guid.NewGuid());
            var meetingId = new MeetingId(Guid.NewGuid());
            var fee = MoneyValue.Of(0, "EUR");

            AssertBrokenRule<MeetingPaymentFeeMustBeGreaterThanZeroRule>(() =>
            {
                MeetingPayment.CreatePaymentForMeeting(payerId, meetingId, fee);
            });
        }

        [Test]
        public void MarkPaymentAsPayed_WhenIsNotPayed_IsPayed()
        {
            var payerId = new PayerId(Guid.NewGuid());
            var meetingId = new MeetingId(Guid.NewGuid());
            var fee = MoneyValue.Of(100, "EUR");
            var paymentDate = DateTime.UtcNow;
            SystemClock.Set(paymentDate);

            var meetingPayment = MeetingPayment.CreatePaymentForMeeting(payerId, meetingId, fee);
            meetingPayment.MarkIsPayed();

            var meetingPayed = AssertPublishedDomainEvent<MeetingPayedDomainEvent>(meetingPayment);

            Assert.That(meetingPayed.MeetingId, Is.EqualTo(meetingId));
            Assert.That(meetingPayed.PayerId, Is.EqualTo(payerId));
            Assert.That(meetingPayed.PaymentDate, Is.EqualTo(paymentDate));
        }

        [Test]
        public void MarkPaymentAsPayed_WhenIsPayedAlready_CannotBePayedTwice()
        {
            var payerId = new PayerId(Guid.NewGuid());
            var meetingId = new MeetingId(Guid.NewGuid());
            var fee = MoneyValue.Of(100, "EUR");

            var meetingPayment = MeetingPayment.CreatePaymentForMeeting(payerId, meetingId, fee);
            meetingPayment.MarkIsPayed();

            AssertBrokenRule<MeetingPaymentCannotBePayedTwiceRule>(() =>
            {
                meetingPayment.MarkIsPayed();
            }); 
        }
    }
}