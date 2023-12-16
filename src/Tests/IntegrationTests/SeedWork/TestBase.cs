using System.Data;
using System.Data.SqlClient;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using Dapper;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IAdministrationModule AdministrationModule { get; private set; }

        protected IMeetingsModule MeetingsModule { get; private set; }

        protected IEmailSender EmailSender { get; private set; }

        protected ExecutionContextMock ExecutionContext { get; private set; }

        protected IEventsBus EventsBus { get; private set; }

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                await ClearDatabase(sqlConnection);
            }

            Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            EmailSender = Substitute.For<IEmailSender>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            EventsBus = new InMemoryEventBusClient(Logger);

            AdministrationStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                EventsBus);

            MeetingsStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                new EmailsConfiguration("from@email.com"),
                EventsBus);

            AdministrationModule = new AdministrationModule();
            MeetingsModule = new MeetingsModule();
        }

        [TearDown]
        public void AfterEachTest()
        {
            MeetingsStartup.Stop();
            AdministrationStartup.Stop();
            SystemClock.Reset();
        }

        protected static void AssertBrokenRule<TRule>(AsyncTestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException =
                Assert.CatchAsync<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }

        protected static async Task AssertEventually(IProbe probe, int timeout)
        {
            await new Poller(timeout).CheckAsync(probe);
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [administration].[InboxMessages] " +
                               "DELETE FROM [administration].[InternalCommands] " +
                               "DELETE FROM [administration].[OutboxMessages] " +
                               "DELETE FROM [administration].[MeetingGroupProposals] " +
                               "DELETE FROM [administration].[Members] " +
                               "DELETE FROM [meetings].[InboxMessages] " +
                               "DELETE FROM [meetings].[InternalCommands] " +
                               "DELETE FROM [meetings].[OutboxMessages] " +
                               "DELETE FROM [meetings].[MeetingAttendees] " +
                               "DELETE FROM [meetings].[MeetingGroupMembers] " +
                               "DELETE FROM [meetings].[MeetingGroupProposals] " +
                               "DELETE FROM [meetings].[MeetingGroups] " +
                               "DELETE FROM [meetings].[MeetingNotAttendees] " +
                               "DELETE FROM [meetings].[MeetingCommentingConfigurations] " +
                               "DELETE FROM [meetings].[Meetings] " +
                               "DELETE FROM [meetings].[MeetingWaitlistMembers] " +
                               "DELETE FROM [meetings].[Members] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}