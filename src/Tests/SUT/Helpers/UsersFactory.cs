using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser;
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
            IRegistrationsModule registrationsModule,
            string connectionString,
            string login,
            string password,
            string firstName,
            string lastName,
            string email)
        {
            var userRegistrationId = await registrationsModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                login,
                password,
                email,
                firstName,
                lastName,
                email));

            await registrationsModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));

            await AsyncOperationsHelper.WaitForProcessing(connectionString);

            return userRegistrationId;
        }
    }
}