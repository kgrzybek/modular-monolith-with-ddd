using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserRegistrationConfirmedNotificationHandler : INotificationHandler<UserRegistrationConfirmedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public UserRegistrationConfirmedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public Task Handle(UserRegistrationConfirmedNotification notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new CreateUserCommand(Guid.NewGuid(), notification.DomainEvent.UserRegistrationId));

            return Task.CompletedTask;
        }
    }
}