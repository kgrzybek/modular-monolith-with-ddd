using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand : CommandBase<Result>
{
    public ConfirmUserRegistrationCommand(Guid userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public Guid UserRegistrationId { get; }
}