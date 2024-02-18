using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredNotification : DomainNotificationBase<NewUserRegisteredDomainEvent>
    {
        [JsonConstructor]
        public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}