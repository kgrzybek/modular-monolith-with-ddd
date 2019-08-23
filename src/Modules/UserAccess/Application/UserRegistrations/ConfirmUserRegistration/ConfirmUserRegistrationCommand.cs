using System;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration
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