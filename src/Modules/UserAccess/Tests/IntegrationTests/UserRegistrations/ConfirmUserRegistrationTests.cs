using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.GetUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.Domain.UserRegistrations;
using CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class ConfirmUserRegistrationTests : TestBase
    {
        [Test]
        public async Task ConfirmUserRegistration_Test()
        {
            var registrationId = await UserAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName,
                "confirmLink"));

            await UserAccessModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(registrationId));

            var userRegistration = await UserAccessModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

            Assert.That(userRegistration.StatusCode, Is.EqualTo(UserRegistrationStatus.Confirmed.Value));
        }
    }
}