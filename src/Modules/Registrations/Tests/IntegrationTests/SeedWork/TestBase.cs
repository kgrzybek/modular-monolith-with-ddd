using System.Data;
using System.Data.SqlClient;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests;
using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration;
using Dapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IRegistrationsModule RegistrationsModule { get; private set; }

        protected IEmailSender EmailSender { get; private set; }

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

            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();

            RegistrationsStartup.Initialize(
                ConnectionString,
                new ExecutionContextMock(Guid.NewGuid()),
                Logger,
                new EmailsConfiguration("from@email.com"),
                "key",
                EmailSender,
                null);

            RegistrationsModule = new RegistrationsModule();
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

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [registrations].[InboxMessages] " +
                               "DELETE FROM [registrations].[InternalCommands] " +
                               "DELETE FROM [registrations].[OutboxMessages] " +
                               "DELETE FROM [registrations].[UserRegistrations] ";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}