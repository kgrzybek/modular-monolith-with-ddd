using CompanyName.MyMeetings.Contracts.V1.Users.Users;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Users.Validators;

internal class CreateUserAccountRequestValidator : AbstractValidator<CreateUserAccountRequest>
{
    public CreateUserAccountRequestValidator()
    {
        RuleFor(x => x.UserName).CustomNotEmpty();
        RuleFor(x => x.EmailAddress).CustomEmailAddress();
    }
}