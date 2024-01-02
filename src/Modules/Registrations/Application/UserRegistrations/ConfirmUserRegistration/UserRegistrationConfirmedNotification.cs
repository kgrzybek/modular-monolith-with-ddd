using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;

public class UserRegistrationConfirmedNotification : DomainNotificationBase<UserRegistrationConfirmedDomainEvent>
{
    public UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent, Guid id)
        : base(domainEvent, id)
    {
    }
}