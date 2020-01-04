using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.IntegrationEvents;
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

        public Task Handle(NewUserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _commandsScheduler.EnqueueAsync(new 
                CreatePayerCommand(
                    Guid.NewGuid(),
                    notification.UserId, 
                    notification.Login,
                    notification.Email, 
                    notification.FirstName, 
                    notification.LastName, 
                    notification.Name));

            return Task.CompletedTask;
        }
    }
}