using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Rules;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.UnitTests.MeetingGroupPaymentRegisters
{
    [TestFixture]
    public class MeetingGroupPaymentRegistersTests : TestBase
    {
        [Test]
        public void CreatePaymentScheduleForMeetingGroup_IsSuccessful()
        {
            var meetingGroupId = new MeetingGroupId(Guid.NewGuid());
            var paymentScheduleForMeetingGroup = MeetingGroupPaymentRegister.CreatePaymentScheduleForMeetingGroup(meetingGroupId);

            var meetingGroupPaymentRegisterCreated =
                AssertPublishedDomainEvent<MeetingGroupPaymentRegisterCreatedDomainEvent>(paymentScheduleForMeetingGroup);

            Assert.That(meetingGroupPaymentRegisterCreated.MeetingGroupPaymentRegisterId.Value, Is.EqualTo(meetingGroupId.Value));
        }

        [Test]
        public void RegisterPayment_OnlyOnePayment_IsSuccessful()
        {
            var meetingGroupId = new MeetingGroupId(Guid.NewGuid());
            var paymentScheduleForMeetingGroup = MeetingGroupPaymentRegister.CreatePaymentScheduleForMeetingGroup(meetingGroupId);

            var payerId = new PayerId(Guid.NewGuid());
            var paymentTerm = PaymentTerm.Create(
                new DateTime(2019, 1, 1), 
                new DateTime(2019, 1, 31));
            paymentScheduleForMeetingGroup.RegisterPayment(
                paymentTerm, payerId);

            var paymentRegistered = AssertPublishedDomainEvent<PaymentRegisteredDomainEvent>(paymentScheduleForMeetingGroup);

            Assert.That(paymentRegistered.MeetingGroupPaymentRegisterId, Is.EqualTo(paymentScheduleForMeetingGroup.Id));
            Assert.That(paymentRegistered.DateTo, Is.EqualTo(paymentTerm.EndDate));
        }

        [Test]
        public void RegisterPayment_TermsAreNotOverlapping_IsSuccessful()
        {
            var meetingGroupId = new MeetingGroupId(Guid.NewGuid());
            var paymentScheduleForMeetingGroup = MeetingGroupPaymentRegister.CreatePaymentScheduleForMeetingGroup(meetingGroupId);

            var payerId = new PayerId(Guid.NewGuid());
            var paymentTerm = PaymentTerm.Create(
                new DateTime(2019, 1, 1), 
                new DateTime(2019, 1, 31));
            paymentScheduleForMeetingGroup.RegisterPayment(
                paymentTerm, payerId);

            var nextPaymentTerm = PaymentTerm.Create(
                new DateTime(2019, 2, 1), 
                new DateTime(2019, 2, 28));

            paymentScheduleForMeetingGroup.RegisterPayment(
                nextPaymentTerm, payerId);

            var domainEvents = AssertPublishedDomainEvents<PaymentRegisteredDomainEvent>(paymentScheduleForMeetingGroup);

            Assert.That(domainEvents.Count, Is.EqualTo(2));
            Assert.That(domainEvents[1].DateTo, Is.EqualTo(nextPaymentTerm.EndDate));
        }

        [Test]
        public void RegisterPayment_TermsAreOverlapping_BreaksPaymentTermsCannotOverlapRule()
        {
            var meetingGroupId = new MeetingGroupId(Guid.NewGuid());
            var paymentScheduleForMeetingGroup = MeetingGroupPaymentRegister.CreatePaymentScheduleForMeetingGroup(meetingGroupId);

            var payerId = new PayerId(Guid.NewGuid());
            var paymentTerm = PaymentTerm.Create(
                new DateTime(2019, 1, 1), 
                new DateTime(2019, 1, 31));
            paymentScheduleForMeetingGroup.RegisterPayment(
                paymentTerm, payerId);

            var nextPaymentTerm = PaymentTerm.Create(
                new DateTime(2019, 1, 31), 
                new DateTime(2019, 2, 28));

            AssertBrokenRule<MeetingGroupPaymentsCannotOverlapRule>(() =>
            {
                paymentScheduleForMeetingGroup.RegisterPayment(
                    nextPaymentTerm, payerId);
            });
        }
    }
}