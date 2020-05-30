using System;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    public class UserRegistrationConfirmedNotification : DomainNotificationBase<UserRegistrationConfirmedDomainEvent>
    {
        [JsonConstructor]
        public UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent, Guid id) : base(domainEvent, id)
        {
        }
    }
}