using CompanyName.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredEnqueueEmailConfirmationHandler : INotificationHandler<NewUserRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public NewUserRegisteredEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new SendUserRegistrationConfirmationEmailCommand(
                Guid.NewGuid(),
                notification.DomainEvent.UserRegistrationId,
                notification.DomainEvent.Email,
                notification.DomainEvent.ConfirmLink));
        }
    }
}