using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.CreateUser;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.Users
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public async Task CreateUser_Test()
        {
            var userId = Guid.NewGuid();
            await UserAccessModule.ExecuteCommandAsync(new CreateUserCommand(
                userId,
                UserSampleData.Login,
                UserSampleData.Email,
                UserSampleData.FirstName,
                UserSampleData.LastName,
                UserSampleData.Password));

            var user = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery(userId));

            Assert.That(user.Login, Is.EqualTo(UserSampleData.Login));
            Assert.That(user.Email, Is.EqualTo(UserSampleData.Email));
            Assert.That(user.Name, Is.EqualTo($"{UserSampleData.FirstName} {UserSampleData.LastName}"));
        }
    }

    public struct UserSampleData
    {
        public static string Login => "jdoe";

        public static string Email => "jdoe@mail.com";

        public static string FirstName => "John";

        public static string LastName => "Doe";

        public static string Password => "qwerty";
    }
}