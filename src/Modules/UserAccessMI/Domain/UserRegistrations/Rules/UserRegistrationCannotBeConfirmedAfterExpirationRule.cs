using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Rules;

public class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedAfterExpirationRule(UserRegistrationStatus actualRegistrationStatus)
    {
        this._actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public string Message => "User Registration cannot be confirmed because it is expired";
}