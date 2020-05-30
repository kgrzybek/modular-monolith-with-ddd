using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.CreatePaymentRegister;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPayment;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.GetMeetingGroupPaymentRegister;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Events;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.MeetingGroupPayments
{
    [TestFixture]
    public class CreatePaymentRegisterTests : TestBase
    {
        [Test]
        public async Task CreatePaymentRegister_Test()
        {
            var meetingGroupProposalId = Guid.NewGuid();
            var registerId = await PaymentsModule.ExecuteCommandAsync(
                new CreatePaymentRegisterCommand(Guid.NewGuid(), meetingGroupProposalId));

            var paymentRegister = await PaymentsModule.ExecuteQueryAsync(new GetMeetingGroupPaymentRegisterQuery(registerId));

            Assert.That(paymentRegister.Id, Is.EqualTo(registerId));
        }

        [Test]
        public async Task RegisterPayment_Test()
        {
            var meetingGroupProposalId = Guid.NewGuid();

            var registerId = await PaymentsModule.ExecuteCommandAsync(
                new CreatePaymentRegisterCommand(Guid.NewGuid(), meetingGroupProposalId));

            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 31);
            
            await PaymentsModule.ExecuteCommandAsync(new RegisterPaymentCommand(registerId, startDate, endDate));

            var payments = await PaymentsModule.ExecuteQueryAsync(new GetMeetingGroupPaymentsQuery(registerId));

            Assert.That(payments[0].MeetingGroupPaymentRegisterId, Is.EqualTo(registerId));
            Assert.That(payments[0].PaymentTermStartDate, Is.EqualTo(startDate));
            Assert.That(payments[0].PaymentTermEndDate, Is.EqualTo(endDate));
            Assert.That(payments[0].PayerId, Is.EqualTo(ExecutionContext.UserId));

            var paymentRegisteredNotification = await GetLastOutboxMessage<PaymentRegisteredNotification>();

            Assert.That(paymentRegisteredNotification.DateTo, Is.EqualTo(endDate));
            Assert.That(paymentRegisteredNotification.MeetingGroupPaymentRegisterId.Value, Is.EqualTo(registerId));
        }

        [Test]
        public async Task PaymentRegisteredNotification_Test()
        {
            var paymentRegisteredNotificationHandler = new PaymentRegisteredNotificationHandler(EventsBus);

            var meetingGroupPaymentRegisterId = new MeetingGroupPaymentRegisterId(Guid.NewGuid());
            var dateTo = new DateTime(2020, 1, 31);
            var notification = new PaymentRegisteredNotification(new PaymentRegisteredDomainEvent(meetingGroupPaymentRegisterId, dateTo), Guid.NewGuid());
            
            await paymentRegisteredNotificationHandler.Handle(notification, CancellationToken.None);

            var paymentRegisteredIntegrationEvent = EventsBus.GetLastPublishedEvent<PaymentRegisteredIntegrationEvent>();
            Assert.That(paymentRegisteredIntegrationEvent.MeetingGroupPaymentRegisterId, Is.EqualTo(meetingGroupPaymentRegisterId.Value));
            Assert.That(paymentRegisteredIntegrationEvent.DateTo, Is.EqualTo(dateTo));
        }
    }
}