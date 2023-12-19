using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users;
using MediatR;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.ConfirmUserRegistration
{
    public class UserRegistrationConfirmedHandler : INotificationHandler<UserRegistrationConfirmedDomainEvent>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        private readonly IUserRepository _userRepository;

        public UserRegistrationConfirmedHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegistrationConfirmedDomainEvent @event, CancellationToken cancellationToken)
        {
            var userRegistration = await _userRegistrationRepository.GetByIdAsync(@event.UserRegistrationId);

            var user = userRegistration.CreateUser();

            await _userRepository.AddAsync(user);
        }
    }
}