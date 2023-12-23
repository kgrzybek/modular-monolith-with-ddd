using System.Reflection;
using CompanyName.MyMeetings.ArchTests.SeedWork;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure;
using MediatR;
using NetArchTest.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.ArchTests.Modules
{
    [TestFixture]
    public class ModuleTests : TestBase
    {
        [Test]
        public void AdministrationModule_DoesNotHave_Dependency_On_Other_Modules()
        {
            List<string> otherModules = [MeetingsNamespace, PaymentsNamespace, UserAccessNamespace];
            List<Assembly> administrationAssemblies =
            [
                typeof(IAdministrationModule).Assembly,
                typeof(MeetingGroupLocation).Assembly,
                typeof(AdministrationContext).Assembly
            ];

            var result = Types.InAssemblies(administrationAssemblies)
                .That()
                    .DoNotImplementInterface(typeof(INotificationHandler<>))
                    .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
                    .And().DoNotHaveName("EventsBusStartup")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void MeetingsModule_DoesNotHave_Dependency_On_Other_Modules()
        {
            List<string> otherModules = [AdministrationNamespace, PaymentsNamespace, UserAccessNamespace];
            List<Assembly> meetingsAssemblies =
            [
                typeof(IMeetingsModule).Assembly,
                typeof(Meeting).Assembly,
                typeof(MeetingsContext).Assembly
            ];

            var result = Types.InAssemblies(meetingsAssemblies)
                .That()
                .DoNotImplementInterface(typeof(INotificationHandler<>))
                .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
                .And().DoNotHaveName("EventsBusStartup")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void PaymentsModule_DoesNotHave_Dependency_On_Other_Modules()
        {
            List<string> otherModules = [AdministrationNamespace, MeetingsNamespace, UserAccessNamespace];
            List<Assembly> paymentsAssemblies =
            [
                typeof(IPaymentsModule).Assembly,
                typeof(MeetingFee).Assembly,
                typeof(PaymentsStartup).Assembly
            ];

            var result = Types.InAssemblies(paymentsAssemblies)
                .That()
                .DoNotImplementInterface(typeof(INotificationHandler<>))
                .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
                .And().DoNotHaveName("EventsBusStartup")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }

        [Test]
        public void UserAccessModule_DoesNotHave_Dependency_On_Other_Modules()
        {
            List<string> otherModules = [AdministrationNamespace, MeetingsNamespace, PaymentsNamespace];
            List<Assembly> userAccessAssemblies =
            [
                typeof(IUserAccessModule).Assembly,
                typeof(User).Assembly,
                typeof(UserAccessContext).Assembly
            ];

            var result = Types.InAssemblies(userAccessAssemblies)
                .That()
                .DoNotImplementInterface(typeof(INotificationHandler<>))
                .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
                .And().DoNotHaveName("EventsBusStartup")
                .Should()
                .NotHaveDependencyOnAny(otherModules.ToArray())
                .GetResult();

            AssertArchTestResult(result);
        }
    }
}