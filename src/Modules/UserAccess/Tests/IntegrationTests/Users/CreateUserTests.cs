using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.SeedWork;
using CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.UserRegistrations;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.Users
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