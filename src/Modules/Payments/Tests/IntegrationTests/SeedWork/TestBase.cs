using System.Data;
using System.Data.SqlClient;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using Dapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IPaymentsModule PaymentsModule { get; private set; }

        protected IEmailSender EmailSender { get; private set; }

        protected EmailsConfiguration EmailsConfiguration { get; private set; }

        protected EventsBusMock EventsBus { get; private set; }

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
            EmailsConfiguration = new EmailsConfiguration("from@email.com");
            EventsBus = new EventsBusMock();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            PaymentsStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                EmailsConfiguration,
                EventsBus,
                true);

            PaymentsModule = new PaymentsModule();
        }

        public static async Task<T> GetEventually<T>(IProbe<T> probe, int timeout)
            where T : class
        {
            var poller = new Poller(timeout);

            return await poller.GetAsync(probe);
        }

        [TearDown]
        public void AfterEachTest()
        {
            PaymentsStartup.Stop();
            SystemClock.Reset();
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

        protected static async Task AssertEventually(IProbe probe, int timeout)
        {
            await new Poller(timeout).CheckAsync(probe);
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [payments].[InboxMessages] " +
                               "DELETE FROM [payments].[InternalCommands] " +
                               "DELETE FROM [payments].[OutboxMessages] " +
                               "DELETE FROM payments.Messages " +
                               "DBCC CHECKIDENT ('payments.Messages', RESEED, 0); " +
                               "DELETE FROM payments.Streams " +
                               "DBCC CHECKIDENT ('payments.Streams', RESEED, 0); " +
                               "DELETE FROM payments.SubscriptionDetails " +
                               "DELETE FROM [payments].[SubscriptionCheckpoints] " +
                               "DELETE FROM [payments].PriceListItems " +
                               "DELETE FROM [payments].SubscriptionPayments " +
                               "DELETE FROM [payments].MeetingFees " +
                               "DELETE FROM [payments].[Payers] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}