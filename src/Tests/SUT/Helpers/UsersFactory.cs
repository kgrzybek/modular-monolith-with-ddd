using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class UsersFactory
    {
        public static async Task GivenAdmin(
            IUserAccessModule userAccessModule,
            string login,
            string password,
            string name,
            string firstName,
            string lastName,
            string email)
        {
            await userAccessModule.ExecuteCommandAsync(new AddAdminUserCommand(
                login,
                password,
                firstName,
                lastName,
                name,
                email
            ));
        }
    }
}