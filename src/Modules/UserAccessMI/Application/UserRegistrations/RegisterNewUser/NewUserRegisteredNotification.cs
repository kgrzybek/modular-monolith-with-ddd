using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.RegisterNewUser;

public class NewUserRegisteredNotification : DomainNotificationBase<NewUserRegisteredDomainEvent>
{
    [JsonConstructor]
    public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id)
        : base(domainEvent, id)
    {
    }
}