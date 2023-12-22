using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure;
using CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using CompanyName.MyMeetings.SUT.SeedWork.Probing;
using Dapper;
using NSubstitute;
using NUnit.Framework;
using Serilog;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; set; }

        protected virtual bool PerformDatabaseCleanup => false;

        protected virtual bool CreatePermissions => true;

        protected IEmailSender EmailSender { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IUserAccessModule UserAccessModule { get; private set; }

        protected IMeetingsModule MeetingsModule { get; private set; }
        
        protected IAdministrationModule AdministrationModule { get; private set; }
        
        protected IPaymentsModule PaymentsModule { get; private set; }

        protected ExecutionContextMock ExecutionContextAccessor { get; private set; }

        protected IEventsBus EventsBus { get; private set; }

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

            ExecutionContextAccessor = new ExecutionContextMock(Guid.NewGuid());

            var emailsConfiguration = new EmailsConfiguration("from@email.com");
            
            Logger = Substitute.For<ILogger>();
            
            EventsBus = new InMemoryEventBusClient(Logger);

            InitializeUserAccessModule(emailsConfiguration);

            InitializeMeetingsModule(emailsConfiguration);
            
            InitializeAdministrationModule();

            PaymentsStartup.Initialize(
                ConnectionString,
                ExecutionContextAccessor,
                Logger,
                emailsConfiguration,
                EventsBus, 
                true,
                100);
            
            PaymentsModule = new PaymentsModule();
        }

        private void InitializeAdministrationModule()
        {
            AdministrationStartup.Initialize(
                ConnectionString,
                ExecutionContextAccessor,
                Logger,
                EventsBus,
                100);

            AdministrationModule = new AdministrationModule();
        }

        protected async Task WaitForAsyncOperations()
        {
            await AsyncOperationsHelper.WaitForProcessing(ConnectionString);
        }
        
        protected void SetDate(DateTime date)
        {
            SystemClock.Set(date);
            Modules.Payments.Domain.SeedWork.SystemClock.Set(date);
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
            SystemClock.Reset();
            Modules.Payments.Domain.SeedWork.SystemClock.Reset();
        }

        private void InitializeMeetingsModule(EmailsConfiguration emailsConfiguration)
        {
            MeetingsStartup.Initialize(
                ConnectionString,
                ExecutionContextAccessor,
                Logger,
                emailsConfiguration,
                EventsBus,
                100);

            MeetingsModule = new MeetingsModule();
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

        private void InitializeUserAccessModule(EmailsConfiguration emailsConfiguration)
        {
            Logger = Substitute.For<ILogger>();
            EmailSender = Substitute.For<IEmailSender>();

            UserAccessStartup.Initialize(
                ConnectionString,
                ExecutionContextAccessor,
                Logger,
                emailsConfiguration,
                "key",
                EmailSender,
                EventsBus,
                100);

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