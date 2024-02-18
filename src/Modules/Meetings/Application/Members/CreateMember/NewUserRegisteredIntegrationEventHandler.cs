using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    public class NewUserRegisteredIntegrationEventHandler : INotificationHandler<NewUserRegisteredIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public NewUserRegisteredIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(NewUserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new
                CreateMemberCommand(
                    Guid.NewGuid(),
                    notification.UserId,
                    notification.Login,
                    notification.Email,
                    notification.FirstName,
                    notification.LastName,
                    notification.Name));
        }
    }
}