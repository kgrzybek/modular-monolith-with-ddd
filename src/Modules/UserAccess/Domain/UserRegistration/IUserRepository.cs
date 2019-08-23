using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistration
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);

        Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId);
    }
}