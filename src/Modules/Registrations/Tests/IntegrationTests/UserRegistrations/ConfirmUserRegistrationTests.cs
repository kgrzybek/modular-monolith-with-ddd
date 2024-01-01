using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.GetUserRegistration;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class ConfirmUserRegistrationTests : TestBase
    {
        [Test]
        public async Task ConfirmUserRegistration_Test()
        {
            var registrationId = await RegistrationsModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName,
                "confirmLink"));

            await RegistrationsModule.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(registrationId));

            var userRegistration = await RegistrationsModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

            Assert.That(userRegistration.StatusCode, Is.EqualTo(UserRegistrationStatus.Confirmed.Value));
        }
    }
}