using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Users.GetUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.SeedWork;
using CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.UserRegistrations;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.Users
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public async Task CreateUser_Test()
        {
            var registrationId = await UserAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName,
                "confirmLink"));
            await UserAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(registrationId));

            var user = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery(registrationId));

            Assert.That(user.Login, Is.EqualTo(UserRegistrationSampleData.Login));
        }
    }
}