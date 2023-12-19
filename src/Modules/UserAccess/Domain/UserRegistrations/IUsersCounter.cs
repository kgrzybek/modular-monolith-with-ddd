namespace CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations
{
    public interface IUsersCounter
    {
        int CountUsersWithLogin(string login);
    }
}