using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Infrastructure.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public async Task AddAsync(User user)
        {
            await _userAccessContext.Users.AddAsync(user);
        }
    }
}