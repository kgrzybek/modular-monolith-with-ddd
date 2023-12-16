using CompanyName.MyMeetings.SUT.Helpers;
using CompanyName.MyMeetings.SUT.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.SUT.TestCases
{
    public class OnlyAdminTestCase : TestBase
    {
        protected override bool PerformDatabaseCleanup => true;

        [Test]
        public async Task Prepare()
        {
            await UsersFactory.GivenAdmin(
                UserAccessModule,
                "testAdmin@mail.com",
                "testAdminPass",
                "Jane Doe",
                "Jane",
                "Doe",
                "testAdmin@mail.com");
        }
    }
}