using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication;
using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    internal class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUsersCounter _usersCounter;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUsersCounter usersCounter)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _usersCounter = usersCounter;
        }

        public async Task<Guid> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
        {
            var password = PasswordManager.HashPassword(command.Password);

            var userRegistration = UserRegistration.RegisterNewUser(
                command.Login,
                password,
                command.Email,
                command.FirstName,
                command.LastName,
                _usersCounter,
                command.ConfirmLink);

            await _userRegistrationRepository.AddAsync(userRegistration);

            return userRegistration.Id.Value;
        }
    }
}