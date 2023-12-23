using CompanyName.MyMeetings.ArchTests.SeedWork;
using NetArchTest.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.ArchTests.Api
{
    [TestFixture]
    public class ApiTests : TestBase
    {
        [Test]
        public void AdministrationApi_DoesNotHaveDependency_ToOtherModules()
        {
            List<string> otherModules = [MeetingsNamespace, PaymentsNamespace, UserAccessNamespace];
            var result = Types.InAssembly(ApiAssembly)
                .That()
                        .ResideInNamespace("CompanyName.MyMeetings.API.Modules.Administration")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void MeetingsApi_DoesNotHaveDependency_ToOtherModules()
        {
            List<string> otherModules = [AdministrationNamespace, PaymentsNamespace, UserAccessNamespace];
            var result = Types.InAssembly(ApiAssembly)
                .That()
                .ResideInNamespace("CompanyName.MyMeetings.API.Modules.Meetings")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void PaymentsApi_DoesNotHaveDependency_ToOtherModules()
        {
            List<string> otherModules = [AdministrationNamespace, MeetingsNamespace, UserAccessNamespace];
            var result = Types.InAssembly(ApiAssembly)
                .That()
                .ResideInNamespace("CompanyName.MyMeetings.API.Modules.Payments")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void UserAccessApi_DoesNotHaveDependency_ToOtherModules()
        {
            List<string> otherModules = [AdministrationNamespace, MeetingsNamespace, PaymentsNamespace];
            var result = Types.InAssembly(ApiAssembly)
                .That()
                .ResideInNamespace("CompanyName.MyMeetings.API.Modules.UserAccess")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }
    }
}