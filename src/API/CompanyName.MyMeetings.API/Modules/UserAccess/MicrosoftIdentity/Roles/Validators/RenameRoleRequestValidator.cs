using CompanyName.MyMeetings.Contracts.V1.Users.Roles;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application;
using FluentValidation;

namespace CompanyName.MyMeetings.API.Modules.UserAccess.MicrosoftIdentity.Roles.Validators;

internal class RenameRoleRequestValidator : AbstractValidator<RenameRoleRequest>
{
    public RenameRoleRequestValidator()
    {
        RuleFor(x => x.Name).CustomNotEmpty();
    }
}