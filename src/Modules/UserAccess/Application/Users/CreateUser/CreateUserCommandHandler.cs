using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser;

internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.CreateUser(
            command.UserId,
            command.Login,
            command.Password,
            command.Email,
            command.FirstName,
            command.LastName);

        await _userRepository.AddAsync(user);
    }
}