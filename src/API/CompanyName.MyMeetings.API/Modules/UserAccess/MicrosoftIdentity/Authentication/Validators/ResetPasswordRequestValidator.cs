using CompanyName.MyMeetings.Contracts.V1.Users.Authentication;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authentication.Validators;

internal class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Token).CustomNotEmpty();
        RuleFor(x => x.EmailAddress).CustomEmailAddress();
        RuleFor(x => x.Password).CustomNotEmpty();
        RuleFor(x => x.ConfirmPassword).CustomNotEmpty();

        RuleFor(x => x.ConfirmPassword).CustomEqual(x => x.Password);
    }
}