using System.Reflection;
using CompanyName.MyMeetings.Modules.Administration.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using NetArchTest.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Administration.ArchTests.SeedWork
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CreateMemberCommand).Assembly;

        protected static Assembly DomainAssembly => typeof(MeetingGroupProposal).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(AdministrationContext).Assembly;

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