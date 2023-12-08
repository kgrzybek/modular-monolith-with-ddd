using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeePaymentAsPaid
{
    internal class MarkMeetingFeePaymentAsPaidCommandHandler : ICommandHandler<MarkMeetingFeePaymentAsPaidCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        internal MarkMeetingFeePaymentAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(MarkMeetingFeePaymentAsPaidCommand command, CancellationToken cancellationToken)
        {
            var meetingFeePayment =
                await _aggregateStore.Load(new MeetingFeePaymentId(command.MeetingFeePaymentId));

            meetingFeePayment.MarkAsPaid();

            _aggregateStore.AppendChanges(meetingFeePayment);
        }
    }
}