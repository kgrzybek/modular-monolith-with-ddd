using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using Dapper;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    public class TestBase
    {
        private string ConnectionString { get; set; }
        
        protected virtual bool PerformDatabaseCleanup => false;

        protected virtual bool CreatePermissions => true;
        
        protected IEmailSender EmailSender { get; private set; }
        
        protected ILogger Logger { get; private set; }
        
        protected IUserAccessModule UserAccessModule { get; private set; }

        [SetUp]
        public async Task BeforeEachTest()
        {
            SetConnectionString();

            if (PerformDatabaseCleanup)
            {
                await this.ClearDatabase();
            }

            if (CreatePermissions)
            {
                await this.SeedPermissions();
            }
            
            InitializeUserAccessModule();
        }
        
        protected async Task ExecuteScript(string scriptPath)
        {
            var sql = await File.ReadAllTextAsync(scriptPath);

            await using var sqlConnection = new SqlConnection(ConnectionString);
            await sqlConnection.ExecuteScalarAsync(sql);
        }

        private async Task SeedPermissions()
        {
            await ExecuteScript("Scripts/SeedPermissions.sql");
        }

        private void InitializeUserAccessModule()
        {
            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();

            UserAccessStartup.Initialize(
                ConnectionString,
                new ExecutionContextMock(Guid.NewGuid()),
                Logger,
                new EmailsConfiguration("from@email.com"),
                "key",
                EmailSender);

            UserAccessModule = new UserAccessModule();
        }

        private void SetConnectionString()
        {
            const string connectionStringEnvironmentVariable = "MyMeetings_SUTDatabaseConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to SUT database using environment variable: {connectionStringEnvironmentVariable}");
            }
        }

        private async Task ClearDatabase()
        {
            await using var sqlConnection = new SqlConnection(ConnectionString);
            await DatabaseCleaner.ClearAllData(sqlConnection);
        }
    }
}