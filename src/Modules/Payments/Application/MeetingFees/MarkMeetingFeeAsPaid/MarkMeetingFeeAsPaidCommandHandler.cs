using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    internal class MarkMeetingFeeAsPaidCommandHandler : ICommandHandler<MarkMeetingFeeAsPaidCommand>
    {
        private readonly IAggregateStore _aggregateStore;

        internal MarkMeetingFeeAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(MarkMeetingFeeAsPaidCommand command, CancellationToken cancellationToken)
        {
            var meetingFee =
                await _aggregateStore.Load(new MeetingFeeId(command.MeetingFeeId));

            meetingFee.MarkAsPaid();

            _aggregateStore.AppendChanges(meetingFee);
        }
    }
}