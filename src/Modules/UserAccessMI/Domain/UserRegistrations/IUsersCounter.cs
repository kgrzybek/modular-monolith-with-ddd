namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.UserRegistrations;

public interface IUsersCounter
{
    int CountUsersWithLogin(string login);
}