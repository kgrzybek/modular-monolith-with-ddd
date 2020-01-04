using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Domain.UserRegistrations
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRegistrationRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _userAccessContext.AddAsync(userRegistration);
        }

        public async Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
           return await _userAccessContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}