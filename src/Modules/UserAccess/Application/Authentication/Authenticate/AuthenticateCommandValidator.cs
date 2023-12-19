using FluentValidation;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            this.RuleFor(x => x.Login).NotEmpty().WithMessage("Login cannot be empty");
            this.RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}