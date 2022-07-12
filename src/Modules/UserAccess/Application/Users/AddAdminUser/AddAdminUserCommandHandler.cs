using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser
{
    public class AddAdminUserCommandHandler : ICommandHandler<AddAdminUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddAdminUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddAdminUserCommand command, CancellationToken cancellationToken)
        {
            var password = PasswordManager.HashPassword(command.Password);

            var user = User.CreateAdmin(
                command.Login,
                password,
                command.Email,
                command.FirstName,
                command.LastName,
                command.Name);

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}