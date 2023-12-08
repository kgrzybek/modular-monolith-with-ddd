using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration
{
    internal class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public async Task Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var userRegistration =
                await _userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

            userRegistration.Confirm();
        }
    }
}