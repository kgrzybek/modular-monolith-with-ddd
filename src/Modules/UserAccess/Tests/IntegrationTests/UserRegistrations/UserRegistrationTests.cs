using System.Data.SqlClient;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.GetUserRegistration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.UserRegistrations
{
    [TestFixture]
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public async Task RegisterNewUserCommand_Test()
        {
            const string email = "john@mail.com";
            const string login = "john";
            const string firstName = "John";
            const string lastName = "Smith";

            var registerNewCommand = new RegisterNewUserCommand(
                login, 
                "qwerty",
                email, 
                firstName, 
                lastName);

            var registrationId = await UserAccessModule.ExecuteCommandAsync(registerNewCommand);

            var userRegistration = await UserAccessModule.ExecuteQueryAsync(new GetUserRegistrationQuery(registrationId));

            Assert.That(userRegistration.Email, Is.EqualTo(email));
            Assert.That(userRegistration.Login, Is.EqualTo(login));
            Assert.That(userRegistration.FirstName, Is.EqualTo(firstName));
            Assert.That(userRegistration.LastName, Is.EqualTo(lastName));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);

            Assert.That(messagesList.Count, Is.EqualTo(1));

            var customerRegisteredNotification =
                OutboxMessagesHelper.Deserialize<NewUserRegisteredNotification>(messagesList[0]);

            Assert.That(customerRegisteredNotification.DomainEvent.Login, Is.EqualTo(login));
        }
    }
}