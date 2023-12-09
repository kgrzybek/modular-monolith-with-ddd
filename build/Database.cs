using System.Linq;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public partial class Build
{
    Target CompileDbUpMigrator => _ => _
        .Executes(() =>
        {
            var dbUpMigratorProject = Solution.GetAllProjects("DatabaseMigrator").First();
           
            DotNetBuild(s => s
                .SetProjectFile(dbUpMigratorProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDbUbMigratorBuildDirectory)
            );
        });
    
    [Parameter("Modular Monolith database connection string")] readonly string DatabaseConnectionString;
    Target MigrateDatabase => _ => _
        .Requires(() => DatabaseConnectionString != null)
        .DependsOn(CompileDbUpMigrator)
        .Executes(() =>
        {
            var migrationsPath = DatabaseDirectory / "Migrations";

            DotNet($"{DbUpMigratorPath} {DatabaseConnectionString} {migrationsPath}");
        });
}