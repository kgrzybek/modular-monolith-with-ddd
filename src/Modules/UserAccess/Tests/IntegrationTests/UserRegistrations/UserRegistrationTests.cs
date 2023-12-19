using System.Data.SqlClient;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.GetUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccessIS.Application.UserRegistrations.RegisterNewUser;
using CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public async Task RegisterNewUserCommand_Test()
        {
            var registrationId = await UserAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                UserRegistrationSampleData.Login,
                UserRegistrationSampleData.Password,
                UserRegistrationSampleData.Email,
                UserRegistrationSampleData.FirstName,
                UserRegistrationSampleData.LastName,
                "confirmLink"));

            var userRegistration = await UserAccessModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

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