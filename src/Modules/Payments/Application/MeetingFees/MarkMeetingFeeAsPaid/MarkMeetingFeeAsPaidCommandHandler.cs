using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    internal class MarkMeetingFeeAsPaidCommandHandler : ICommandHandler<MarkMeetingFeeAsPaidCommand, Unit>
    {
        private readonly IAggregateStore _aggregateStore;

        internal MarkMeetingFeeAsPaidCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<Unit> Handle(MarkMeetingFeeAsPaidCommand command, CancellationToken cancellationToken)
        {
            var meetingFee =
                await _aggregateStore.Load(new MeetingFeeId(command.MeetingFeeId));

            meetingFee.MarkAsPaid();

            _aggregateStore.AppendChanges(meetingFee);

            return Unit.Value;
        }
    }
}