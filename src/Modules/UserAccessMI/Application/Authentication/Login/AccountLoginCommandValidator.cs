using FluentValidation;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

public class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
{
    public AccountLoginCommandValidator()
    {
        RuleFor(x => x.Login).CustomNotEmpty();
        RuleFor(x => x.Password).CustomNotEmpty();
    }
}