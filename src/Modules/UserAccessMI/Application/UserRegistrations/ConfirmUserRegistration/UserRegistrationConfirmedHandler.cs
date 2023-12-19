using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations.Events;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.UserRegistrations.ConfirmUserRegistration;

public class UserRegistrationConfirmedHandler : INotificationHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRegistrationConfirmedHandler(
        IUserRegistrationRepository userRegistrationRepository,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task Handle(UserRegistrationConfirmedDomainEvent @event, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository.GetByIdAsync(@event.UserRegistrationId);
        if (userRegistration is null)
        {
            return;
        }

        var user = userRegistration.CreateUser();
        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
        {
            return;
        }

        if (!string.IsNullOrEmpty(userRegistration.Password))
        {
            var passwordResult = await _userManager.AddPasswordAsync(user, userRegistration.Password);
            if (!passwordResult.Succeeded)
            {
                return;
            }
        }

        await _userManager.AddToRoleAsync(user, UserRole.Member.Value);
    }
}