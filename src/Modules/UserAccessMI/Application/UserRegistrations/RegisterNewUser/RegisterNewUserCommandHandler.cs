using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.RegisterNewUser;

internal class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        /*
            TODO: How to handle password encryption
            var password = PasswordManager.HashPassword(command.Password);
        */

        var password = command.Password;

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