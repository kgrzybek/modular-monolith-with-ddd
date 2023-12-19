using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

public class UserRegistrationId : TypedIdValueBase
{
    public UserRegistrationId(Guid value)
        : base(value)
    {
    }
}