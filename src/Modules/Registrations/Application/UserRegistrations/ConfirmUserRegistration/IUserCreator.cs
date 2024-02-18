namespace CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;

public interface IUserCreator
{
    public Task Create(
        Guid userRegistrationId,
        string login,
        string password,
        string email,
        string firstName,
        string lastName);
}