using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistrations.UserRegistration userRegistration);

        Task<UserRegistrations.UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId);
    }
}