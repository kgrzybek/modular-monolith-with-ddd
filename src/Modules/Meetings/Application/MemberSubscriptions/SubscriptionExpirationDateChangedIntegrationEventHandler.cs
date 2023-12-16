using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class SubscriptionExpirationDateChangedIntegrationEventHandler : INotificationHandler<SubscriptionExpirationDateChangedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public SubscriptionExpirationDateChangedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(SubscriptionExpirationDateChangedIntegrationEvent @event, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new ChangeSubscriptionExpirationDateForMemberCommand(
                Guid.NewGuid(),
                new MemberId(@event.PayerId),
                @event.ExpirationDate));
        }
    }
}