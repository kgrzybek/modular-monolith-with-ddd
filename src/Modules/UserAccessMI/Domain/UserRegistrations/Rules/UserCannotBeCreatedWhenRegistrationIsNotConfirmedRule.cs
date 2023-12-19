using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Rules;

public class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(UserRegistrationStatus actualRegistrationStatus)
    {
        this._actualRegistrationStatus = actualRegistrationStatus;
    }

    public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;

    public string Message => "User cannot be created when registration is not confirmed";
}