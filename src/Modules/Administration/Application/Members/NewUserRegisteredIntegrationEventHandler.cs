using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members
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