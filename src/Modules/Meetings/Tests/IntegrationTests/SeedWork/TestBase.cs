using System.Data;
using System.Data.SqlClient;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using Dapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IMeetingsModule MeetingsModule { get; private set; }

        protected IEmailSender EmailSender { get; private set; }

        protected ExecutionContextMock ExecutionContext { get; private set; }

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

            MeetingsStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                new EmailsConfiguration("from@email.com"),
                new EventsBusMock());

            MeetingsModule = new MeetingsModule();
        }

        [TearDown]
        public void AfterEachTest()
        {
            MeetingsStartup.Stop();
            SystemClock.Reset();
        }

        protected async Task ExecuteScript(string scriptPath)
        {
            var sql = await File.ReadAllTextAsync(scriptPath);

            await using var sqlConnection = new SqlConnection(ConnectionString);
            await sqlConnection.ExecuteScalarAsync(sql);
        }

        protected async Task<T> GetLastOutboxMessage<T>()
            where T : class, INotification
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

                return OutboxMessagesHelper.Deserialize<T>(messages.Last());
            }
        }

        protected static void AssertBrokenRule<TRule>(AsyncTestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.CatchAsync<BusinessRuleValidationException>(testDelegate, message);
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
            const string sql = "DELETE FROM [meetings].[InboxMessages] " +
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
                               "DELETE FROM [meetings].[MeetingMemberCommentLikes] " +
                               "DELETE FROM [meetings].[MeetingComments] " +
                               "DELETE FROM [meetings].[Countries] " +
                               "DELETE FROM [meetings].[Members] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}