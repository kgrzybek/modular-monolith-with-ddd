using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistration.Events
{
    public class UserRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public UserRegistrationId UserRegistrationId { get; }
    }
}