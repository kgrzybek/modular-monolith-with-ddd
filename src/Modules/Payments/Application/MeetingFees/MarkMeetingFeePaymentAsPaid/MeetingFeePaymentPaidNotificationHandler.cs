using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid
{
    public class MeetingFeePaymentPaidNotificationHandler : INotificationHandler<MeetingFeePaymentPaidNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        private readonly IAggregateStore _aggregateStore;

        public MeetingFeePaymentPaidNotificationHandler(
            ICommandsScheduler commandsScheduler,
            IAggregateStore aggregateStore)
        {
            _commandsScheduler = commandsScheduler;
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(MeetingFeePaymentPaidNotification notification, CancellationToken cancellationToken)
        {
            var meetingFeePayment =
                await _aggregateStore.Load(new MeetingFeePaymentId(notification.DomainEvent.MeetingFeePaymentId));

            var meetingFeePaymentSnapshot = meetingFeePayment.GetSnapshot();
            await _commandsScheduler.EnqueueAsync(
                new MarkMeetingFeeAsPaidCommand(
                    meetingFeePaymentSnapshot.MeetingFeeId));
        }
    }
}