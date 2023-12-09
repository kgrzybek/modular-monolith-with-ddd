using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFeePayment
{
    internal class CreateMeetingFeePaymentCommandHandler : ICommandHandler<CreateMeetingFeePaymentCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        internal CreateMeetingFeePaymentCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public Task<Guid> Handle(CreateMeetingFeePaymentCommand command, CancellationToken cancellationToken)
        {
            var meetingFeePayment = MeetingFeePayment.Create(new MeetingFeeId(command.MeetingFeeId));

            _aggregateStore.AppendChanges(meetingFeePayment);

            return Task.FromResult(meetingFeePayment.Id);
        }
    }
}