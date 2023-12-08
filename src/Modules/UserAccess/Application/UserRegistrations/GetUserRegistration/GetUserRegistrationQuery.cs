using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.GetUserRegistration
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