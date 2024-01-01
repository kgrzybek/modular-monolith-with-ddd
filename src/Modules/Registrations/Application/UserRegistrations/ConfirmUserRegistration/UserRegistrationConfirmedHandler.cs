using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration
{
    public class UserRegistrationConfirmedHandler : INotificationHandler<UserRegistrationConfirmedDomainEvent>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public UserRegistrationConfirmedHandler(
            IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public Task Handle(UserRegistrationConfirmedDomainEvent @event, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //// var userRegistration = await _userRegistrationRepository.GetByIdAsync(@event.UserRegistrationId);
            //
            // var user = userRegistration.CreateUser();
            //
            // await _userRepository.AddAsync(user);
        }
    }
}