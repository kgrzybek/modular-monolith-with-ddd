namespace CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}