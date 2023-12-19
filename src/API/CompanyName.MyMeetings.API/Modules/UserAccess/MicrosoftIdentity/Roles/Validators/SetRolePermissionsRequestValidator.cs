using CompanyName.MyMeetings.Contracts.V1.Users.Roles;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Roles.Validators;

internal class SetRolePermissionsRequestValidator : AbstractValidator<SetRolePermissionsRequest>
{
    public SetRolePermissionsRequestValidator()
    {
        RuleFor(x => x.Permissions).CustomNotEmpty();
    }
}