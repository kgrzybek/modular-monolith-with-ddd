using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Domain.Repositories;

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

    public Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return _userAccessContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
    }
}