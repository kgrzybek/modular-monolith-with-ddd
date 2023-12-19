using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.RegisterNewUser
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