using CompanyName.MyMeetings.Contracts.V1.Users.Roles;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Roles.Validators;

internal class AddRoleRequestValidator : AbstractValidator<AddRoleRequest>
{
    public AddRoleRequestValidator()
    {
        RuleFor(x => x.Name).CustomNotEmpty();
        RuleFor(x => x.Permissions).CustomNotEmpty();
    }
}
