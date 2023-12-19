using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeExpiredMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeExpiredMoreThanOnceRule(UserRegistrationStatus actualRegistrationStatus)
    {
        this._actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public string Message => "User Registration cannot be expired more than once";
}