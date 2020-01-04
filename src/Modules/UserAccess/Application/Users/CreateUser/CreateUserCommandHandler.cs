using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository, 
            IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRegistration = await _userRegistrationRepository.GetByIdAsync(request.UserRegistrationId);

            var user = userRegistration.CreateUser();

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}