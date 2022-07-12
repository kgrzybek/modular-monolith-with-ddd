using System;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Utils;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public partial class Build
{
    AbsolutePath WorkingDirectory => RootDirectory / ".nuke-working-directory";

    AbsolutePath OutputDirectory => WorkingDirectory / "output";

    AbsolutePath OutputDbUbMigratorBuildDirectory => OutputDirectory / "dbUpMigrator";

    AbsolutePath InputFilesDirectory => WorkingDirectory / "input-files";

    AbsolutePath DatabaseDirectory =>
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
                .SetImage("mcr.microsoft.com/mssql/server:2019-latest")
                .SetEnv(
                    $"SA_PASSWORD={SqlServerPassword}",
                    "ACCEPT_EULA=Y",
                    "MSSQL_PID=Express")
                .SetPublish($"{SqlServerPort}:1433")
                .SetMount($"type=bind,source=\"{InputFilesDirectory}\",target=/{InputFilesDirectoryName},readonly")
                .EnableDetach());

            SqlReadinessChecker.WaitForSqlSever(
                $"Server=127.0.0.1,{SqlServerPort};Database=master;User={SqlServerUser};Password={SqlServerPassword}");
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
            var dbUpMigratorProject = Solution.GetProject("DatabaseMigrator");
            DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUbMigratorBuildDirectory)
            );
        });

    AbsolutePath DbUpMigratorPath => OutputDbUbMigratorBuildDirectory / "DatabaseMigrator.dll";

    readonly string MyMeetingsDatabaseConnectionString = $"Server=127.0.0.1,{SqlServerPort};Database=MyMeetings;User={SqlServerUser};Password={SqlServerPassword}";

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
            var integrationTest = Solution.GetProject(MeetingsModuleIntegrationTestsAssemblyName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    const string MyMeetingsDatabaseEnvName = "ASPNETCORE_MyMeetings_IntegrationTests_ConnectionString";

    Target RunMeetingsModuleIntegrationTests => _ => _
        .DependsOn(BuildMeetingsModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(MeetingsModuleIntegrationTestsAssemblyName);
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
            var integrationTest = Solution.GetProject(AdministrationModuleIntegrationTestsAssemblyName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunAdministrationModuleIntegrationTests => _ => _
        .DependsOn(BuildAdministrationModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(AdministrationModuleIntegrationTestsAssemblyName);
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
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsAssemblyName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunUserAccessModuleIntegrationTests => _ => _
        .DependsOn(BuildUserAccessModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsAssemblyName);
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
            var integrationTest = Solution.GetProject(PaymentsModuleIntegrationTestsAssemblyName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunPaymentsModuleIntegrationTests => _ => _
        .DependsOn(BuildPaymentsModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(PaymentsModuleIntegrationTestsAssemblyName);
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
            var integrationTest = Solution.GetProject(SystemIntegrationTestsAssemblyName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunSystemIntegrationTests => _ => _
        .DependsOn(BuildSystemIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(SystemIntegrationTestsAssemblyName);
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