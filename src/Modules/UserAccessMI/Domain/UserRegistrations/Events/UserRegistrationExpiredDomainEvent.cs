using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Events;

public class UserRegistrationExpiredDomainEvent : DomainEventBase
{
    public UserRegistrationExpiredDomainEvent(UserRegistrationId userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public UserRegistrationId UserRegistrationId { get; }
}