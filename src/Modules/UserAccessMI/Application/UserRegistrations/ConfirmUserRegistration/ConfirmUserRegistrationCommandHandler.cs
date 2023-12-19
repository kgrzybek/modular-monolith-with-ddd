using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.ConfirmUserRegistration;

internal class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand, Result>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;

    public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task<Result> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration =
            await _userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

        if (userRegistration is null)
        {
            return Errors.General.NotFound(request.UserRegistrationId, "User registration");
        }

        userRegistration.Confirm();
        return Result.Ok();
    }
}