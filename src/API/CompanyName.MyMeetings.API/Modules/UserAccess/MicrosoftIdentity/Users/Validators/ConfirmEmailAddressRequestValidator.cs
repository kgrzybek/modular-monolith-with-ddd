using CompanyName.MyMeetings.Contracts.V1.Users.Users;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Users.Validators;

internal class ConfirmEmailAddressRequestValidator : AbstractValidator<ConfirmEmailAddressRequest>
{
    public ConfirmEmailAddressRequestValidator()
    {
        RuleFor(x => x.Token).CustomNotEmpty();
        RuleFor(x => x.EmailAddress).CustomEmailAddress();
    }
}