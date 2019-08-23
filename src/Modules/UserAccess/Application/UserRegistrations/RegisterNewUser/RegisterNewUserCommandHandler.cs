using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Processing;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistration;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public async Task<Unit> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var password = PasswordManager.HashPassword(request.Password);

            var userRegistration = UserRegistration.RegisterNewUser(
                request.Login, 
                password, 
                request.Email, 
                request.FirstName,
                request.LastName);

            await _userRegistrationRepository.AddAsync(userRegistration);

            return Unit.Value;
        }
    }
}