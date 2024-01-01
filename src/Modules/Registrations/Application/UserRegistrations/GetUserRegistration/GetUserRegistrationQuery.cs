using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.GetUserRegistration
{
    public class GetUserRegistrationQuery : QueryBase<UserRegistrationDto>
    {
        public GetUserRegistrationQuery(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; }
    }
}