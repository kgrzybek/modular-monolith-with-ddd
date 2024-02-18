using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer
{
    internal class NewUserRegisteredIntegrationEventHandler : INotificationHandler<NewUserRegisteredIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        internal NewUserRegisteredIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(NewUserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new
                CreatePayerCommand(
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