using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.SeedWork;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredNotification : DomainNotificationBase<NewUserRegisteredDomainEvent>
    {
        [JsonConstructor]
        public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent) : base(domainEvent)
        {
        }
    }
}