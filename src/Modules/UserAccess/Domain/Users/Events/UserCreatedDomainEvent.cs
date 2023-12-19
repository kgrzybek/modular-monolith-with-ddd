using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public UserCreatedDomainEvent(UserId id)
        {
            Id = id;
        }

        public new UserId Id { get; }
    }
}