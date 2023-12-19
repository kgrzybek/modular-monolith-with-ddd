using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users.Events;

public class UserCreatedDomainEvent : DomainEventBase
{
    public UserCreatedDomainEvent(Guid id)
    {
        Id = id;
    }

    public new Guid Id { get; }
}