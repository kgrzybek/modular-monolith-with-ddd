using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.GetUserRegistration
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