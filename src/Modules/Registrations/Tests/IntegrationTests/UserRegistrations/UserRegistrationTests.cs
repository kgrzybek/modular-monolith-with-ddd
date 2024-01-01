using System.Data.SqlClient;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.GetUserRegistration;
using CompanyName.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;
using CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public async Task RegisterNewUserCommand_Test()
        {
            var registrationId = await RegistrationsModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName,
                "confirmLink"));

            var userRegistration = await RegistrationsModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

            Assert.That(userRegistration.Email, Is.EqualTo(UserRegistrationSampleData.Email));
            Assert.That(userRegistration.Login, Is.EqualTo(UserRegistrationSampleData.Login));
            Assert.That(userRegistration.FirstName, Is.EqualTo(UserRegistrationSampleData.FirstName));
            Assert.That(userRegistration.LastName, Is.EqualTo(UserRegistrationSampleData.LastName));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);

            Assert.That(messagesList.Count, Is.EqualTo(1));

            var newUserRegisteredNotification =
                await GetLastOutboxMessage<NewUserRegisteredNotification>();

            Assert.That(newUserRegisteredNotification.DomainEvent.Login, Is.EqualTo(UserRegistrationSampleData.Login));
        }
    }
}