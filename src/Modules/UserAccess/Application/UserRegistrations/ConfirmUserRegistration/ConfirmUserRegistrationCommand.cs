using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.ConfirmUserRegistration
{
    public class ConfirmUserRegistrationCommand : CommandBase
    {
        public ConfirmUserRegistrationCommand(Guid userRegistrationId)
        {
            UserRegistrationId = userRegistrationId;
        }

        public Guid UserRegistrationId { get; }
    }
}