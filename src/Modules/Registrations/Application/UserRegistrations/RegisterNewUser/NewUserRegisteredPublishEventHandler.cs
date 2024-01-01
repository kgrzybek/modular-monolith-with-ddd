using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Registrations.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredPublishEventHandler : INotificationHandler<NewUserRegisteredNotification>
    {
        private readonly IEventsBus _eventsBus;

        public NewUserRegisteredPublishEventHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new NewUserRegisteredIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.UserRegistrationId.Value,
                notification.DomainEvent.Login,
                notification.DomainEvent.Email,
                notification.DomainEvent.FirstName,
                notification.DomainEvent.LastName,
                notification.DomainEvent.Name));
        }
    }
}