using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Domain.UserRegistrations
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly RegistrationsContext _context;

        public UserRegistrationRepository(RegistrationsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserRegistration userRegistration)
        {
            await _context.AddAsync(userRegistration);
        }

        public async Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
            return await _context.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}