using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using Dapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.Modules.Administration.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        protected ILogger Logger;

        protected IAdministrationModule AdministrationModule;

        protected IEmailSender EmailSender;

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
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            AdministrationStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            AdministrationModule = new AdministrationModule();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [administration].[InboxMessages] " +
                               "DELETE FROM [administration].[InternalCommands] " +
                               "DELETE FROM [administration].[OutboxMessages] " +
                               "DELETE FROM [administration].[MeetingGroupProposals] " +
                               "DELETE FROM [administration].[Members] ";

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