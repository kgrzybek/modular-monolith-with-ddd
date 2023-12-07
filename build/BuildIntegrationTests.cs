using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Utils;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public partial class Build
{
    static AbsolutePath WorkingDirectory => RootDirectory / ".nuke-working-directory";

    static AbsolutePath OutputDirectory => WorkingDirectory / "output";

    static AbsolutePath OutputDbUbMigratorBuildDirectory => OutputDirectory / "dbUpMigrator";

    static AbsolutePath InputFilesDirectory => WorkingDirectory / "input-files";

    static AbsolutePath DatabaseDirectory =>
        RootDirectory / "src" / "Database" / "CompanyName.MyMeetings.Database" / "Scripts";

    const string CreateDatabaseScriptName = "CreateDatabase_Linux.sql";

    const string InputFilesDirectoryName = "input-files";

    Target PrepareInputFiles => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            string createDatabaseFile = DatabaseDirectory / CreateDatabaseScriptName;
            string createDatabaseFileTarget = InputFilesDirectory / CreateDatabaseScriptName;
            CopyFile(createDatabaseFile, createDatabaseFileTarget, FileExistsPolicy.Overwrite);
        });

    const string SqlServerPassword = "123qwe!@#QWE";

    const string SqlServerUser = "sa";

    const string SqlServerPort = "1401";

    Target PrepareSqlServer => _ => _
        .DependsOn(PrepareInputFiles)
        .Executes(() =>
        {
            DockerTasks.DockerRun(s => s
                .EnableRm()
                .SetName("sql-server-db")
                .SetImage("mcr.microsoft.com/mssql/server")
                .SetEnv(
                    $"SA_PASSWORD={SqlServerPassword}",
                    "ACCEPT_EULA=Y",
                    "MSSQL_PID=Express")
                .SetPublish($"{SqlServerPort}:1433")
                .SetMount($"type=bind,source=\"{InputFilesDirectory}\",target=/{InputFilesDirectoryName},readonly")
                .EnableDetach());

            SqlReadinessChecker.WaitForSqlSever(
                $"Server=127.0.0.1,{SqlServerPort};Database=master;User={SqlServerUser};Password={SqlServerPassword};Encrypt=False;");
        });

    Target CreateDatabase => _ => _
        .DependsOn(PrepareSqlServer)
        .Executes(() =>
        {
            DockerTasks.DockerExec(s => s
                .EnableInteractive()
                .SetContainer("sql-server-db")
                .SetCommand("/bin/sh")
                .SetArgs("-c", $"./opt/mssql-tools/bin/sqlcmd -d master -i ./{InputFilesDirectoryName}/{CreateDatabaseScriptName} -U {SqlServerUser} -P {SqlServerPassword}"));
        });

    Target CompileDbUpMigratorForIntegrationTests => _ => _
        .DependsOn(CreateDatabase)
        .Executes(() =>
        {
            var dbUpMigratorProject = Solution.GetAllProjects("DatabaseMigrator").First();
            DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUbMigratorBuildDirectory)
            );
        });

    AbsolutePath DbUpMigratorPath => OutputDbUbMigratorBuildDirectory / "DatabaseMigrator.dll";

    readonly string MyMeetingsDatabaseConnectionString = $"Server=127.0.0.1,{SqlServerPort};Database=MyMeetings;User={SqlServerUser};Password={SqlServerPassword};Encrypt=False;";

    Target RunDatabaseMigrations => _ => _
        .DependsOn(CompileDbUpMigratorForIntegrationTests)
        .Executes(() =>
        {
            AbsolutePath migrationsPath = DatabaseDirectory / "Migrations";

            DotNet($"{DbUpMigratorPath} {MyMeetingsDatabaseConnectionString} {migrationsPath}");
        });

    const string MeetingsModuleIntegrationTestsAssemblyName = "CompanyName.MyMeetings.Modules.Meetings.IntegrationTests";

    Target BuildMeetingsModuleIntegrationTests => _ => _
        .DependsOn(RunDatabaseMigrations)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(MeetingsModuleIntegrationTestsAssemblyName).First();

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    const string MyMeetingsDatabaseEnvName = "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";

    Target RunMeetingsModuleIntegrationTests => _ => _
        .DependsOn(BuildMeetingsModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(MeetingsModuleIntegrationTestsAssemblyName).First();
            Environment.SetEnvironmentVariable(
                MyMeetingsDatabaseEnvName,
                MyMeetingsDatabaseConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string AdministrationModuleIntegrationTestsAssemblyName = "CompanyName.MyMeetings.Modules.Administration.IntegrationTests";

    Target BuildAdministrationModuleIntegrationTests => _ => _
        .DependsOn(RunDatabaseMigrations)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(AdministrationModuleIntegrationTestsAssemblyName).First();

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunAdministrationModuleIntegrationTests => _ => _
        .DependsOn(BuildAdministrationModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(AdministrationModuleIntegrationTestsAssemblyName).First();
            Environment.SetEnvironmentVariable(
                MyMeetingsDatabaseEnvName,
                MyMeetingsDatabaseConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string UserAccessModuleIntegrationTestsAssemblyName = "CompanyNames.MyMeetings.Modules.UserAccess.IntegrationTests";

    Target BuildUserAccessModuleIntegrationTests => _ => _
        .DependsOn(RunDatabaseMigrations)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(UserAccessModuleIntegrationTestsAssemblyName).First();

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunUserAccessModuleIntegrationTests => _ => _
        .DependsOn(BuildUserAccessModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(UserAccessModuleIntegrationTestsAssemblyName).First();
            Environment.SetEnvironmentVariable(
                MyMeetingsDatabaseEnvName,
                MyMeetingsDatabaseConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string PaymentsModuleIntegrationTestsAssemblyName = "CompanyName.MyMeetings.Modules.Payments.IntegrationTests";

    Target BuildPaymentsModuleIntegrationTests => _ => _
        .DependsOn(RunDatabaseMigrations)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(PaymentsModuleIntegrationTestsAssemblyName).First();

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunPaymentsModuleIntegrationTests => _ => _
        .DependsOn(BuildPaymentsModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(PaymentsModuleIntegrationTestsAssemblyName).First();
            Environment.SetEnvironmentVariable(
                MyMeetingsDatabaseEnvName,
                MyMeetingsDatabaseConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string SystemIntegrationTestsAssemblyName = "CompanyName.MyMeetings.IntegrationTests";

    Target BuildSystemIntegrationTests => _ => _
        .DependsOn(RunDatabaseMigrations)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(SystemIntegrationTestsAssemblyName).First();

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunSystemIntegrationTests => _ => _
        .DependsOn(BuildSystemIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetAllProjects(SystemIntegrationTestsAssemblyName).First();
            Environment.SetEnvironmentVariable(
                MyMeetingsDatabaseEnvName,
                MyMeetingsDatabaseConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    Target RunAllIntegrationTests => _ => _
        .DependsOn(
            RunAdministrationModuleIntegrationTests,
            RunMeetingsModuleIntegrationTests,
            RunPaymentsModuleIntegrationTests,
            RunUserAccessModuleIntegrationTests,
            RunSystemIntegrationTests)
        .Executes(() =>
        {

        });
}