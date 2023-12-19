using CompanyName.MyMeetings.Contracts.V1.Users.Authentication;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Authentication.Validators;

internal class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(x => x.UserName).CustomNotEmpty();
        RuleFor(x => x.Password).CustomNotEmpty();
    }
}