namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

public interface IUserRegistrationRepository
{
    Task AddAsync(UserRegistration userRegistration);

    Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId);
}