using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Domain.Users;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using Dapper;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        protected ILogger Logger;

        protected IUserAccessModule UserAccessModule;


        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable);
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
            UserAccessStartup.Initialize(
                ConnectionString,
                new ExecutionContextMock(Guid.NewGuid()),
                Logger,
                new EmailsConfiguration("from@email.com"),
                "key");

            UserAccessModule = new UserAccessModule();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [users].[InboxMessages] " +
                               "DELETE FROM [users].[InternalCommands] " +
                               "DELETE FROM [users].[OutboxMessages] " +
                               "DELETE FROM [users].[UserRegistrations] " +
                               "DELETE FROM [users].[Users] " +
                               "DELETE FROM [users].[RolesToPermissions] " +
                               "DELETE FROM [users].[UserRoles] " +
                               "DELETE FROM [users].[Permissions] ";

           await connection.ExecuteScalarAsync(sql);
        }
    }
}