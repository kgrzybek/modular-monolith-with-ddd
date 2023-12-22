using CompanyName.MyMeetings.SUT.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.SUT.TestCases
{
    public class CleanDatabaseTestCase : TestBase
    {
        protected override bool PerformDatabaseCleanup => true;

        protected override bool CreatePermissions => false;

        [Test]
        public void Prepare()
        {
        }
    }
}