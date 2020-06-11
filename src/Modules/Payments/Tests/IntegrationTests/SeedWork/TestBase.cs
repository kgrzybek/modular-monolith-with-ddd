using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
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
        protected string ConnectionString;

        protected ILogger Logger;

        protected IPaymentsModule PaymentsModule;

        protected IEmailSender EmailSender;

        protected EventsBusMock EventsBus;

        protected ExecutionContextMock ExecutionContext;


        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable, EnvironmentVariableTarget.Machine);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                await ClearDatabase(sqlConnection);
            }

            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();
            EventsBus = new EventsBusMock();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
            
            PaymentsStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger,
                EventsBus,
                false);

            PaymentsModule = new PaymentsModule();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [payments].[InboxMessages] " +
                               "DELETE FROM [payments].[InternalCommands] " +
                               "DELETE FROM [payments].[OutboxMessages] " +
                               "DELETE FROM [payments].[MeetingPayments] " +
                               "DELETE FROM [payments].[MeetingGroupPayments] " +
                               "DELETE FROM [payments].[MeetingGroupPaymentRegisters] " +
                               "DELETE FROM dbo.Messages " +
                               "DBCC CHECKIDENT ('dbo.Messages', RESEED, 0); " +
                               "DELETE FROM dbo.Streams " +
                               "DBCC CHECKIDENT ('dbo.Streams', RESEED, 0); " +
                               "DELETE FROM [payments].[Payers] ";

            await connection.ExecuteScalarAsync(sql);
        }

        protected async Task<T> GetLastOutboxMessage<T>() where T : class, INotification
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

                return OutboxMessagesHelper.Deserialize<T>(messages.Last());
            }
        }
    }
}