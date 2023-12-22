using System.Reflection;
using CompanyName.MyMeetings.API;
using NetArchTest.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.ArchTests.SeedWork
{
    public abstract class TestBase
    {
        protected static Assembly ApiAssembly => typeof(Startup).Assembly;

        public const string MeetingsNamespace = "CompanyName.MyMeetings.Modules.Meetings";

        public const string AdministrationNamespace = "CompanyName.MyMeetings.Modules.Administration";

        public const string PaymentsNamespace = "CompanyName.MyMeetings.Modules.Payments";

        public const string UserAccessNamespace = "CompanyName.MyMeetings.Modules.UserAccess";

        protected static void AssertAreImmutable(IEnumerable<Type> types)
        {
            List<Type> failingTypes = [];
            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }

            AssertFailingTypes(failingTypes);
        }

        protected static void AssertFailingTypes(IEnumerable<Type> types)
        {
            Assert.That(types, Is.Null.Or.Empty);
        }

        protected static void AssertArchTestResult(TestResult result)
        {
            AssertFailingTypes(result.FailingTypes);
        }
    }
}