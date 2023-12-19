using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Users.AddAdminUser;
using CompanyName.MyMeetings.SUT.SeedWork;

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
                email));
        }

        public static async Task<Guid> GivenUser(
            IUserAccessModule userAccessModule,
            string connectionString,
            string login,
            string password,
            string firstName,
            string lastName,
            string email)
        {
            var userRegistrationId = await userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                login,
                password,
                email,
                firstName,
                lastName,
                email));

            await userAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            await AsyncOperationsHelper.WaitForProcessing(connectionString);

            return userRegistrationId;
        }
    }
}