using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFee
{
    internal class CreateMeetingFeeCommandHandler : ICommandHandler<CreateMeetingFeeCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        internal CreateMeetingFeeCommandHandler(
            IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public Task<Guid> Handle(CreateMeetingFeeCommand command, CancellationToken cancellationToken)
        {
            var meetingFee = MeetingFee.Create(
                new PayerId(command.PayerId),
                new MeetingId(command.MeetingId),
                MoneyValue.Of(command.Value, command.Currency));

            _aggregateStore.AppendChanges(meetingFee);

            return Task.FromResult(meetingFee.Id);
        }
    }
}