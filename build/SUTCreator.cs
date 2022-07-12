using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Utils;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Nuke.Common.Tools.DotNet;


public partial class Build
{
    [Parameter("SUT creator test name to execute")] readonly string SUTTestName;

    readonly IDictionary<string, string> TestCasesMap = new Dictionary<string, string>
    {
        {"CleanDatabase", "CompanyName.MyMeetings.SUT.TestCases.CleanDatabaseTestCase.Prepare"},
        {"OnlyAdmin", "CompanyName.MyMeetings.SUT.TestCases.OnlyAdminTestCase.Prepare"}
    };
    
    Target PrepareSUT => _ => _
        .Requires(() => SUTTestName != null)
        .Requires(() => DatabaseConnectionString != null)
        .Executes(() =>
        {
            Environment.SetEnvironmentVariable(
                "MyMeetings_SUTDatabaseConnectionString", 
                DatabaseConnectionString,
                EnvironmentVariableTarget.Process);
           
            var sutTestProject = Solution.GetProject("CompanyName.MyMeetings.SUT");

            var fullyQualifiedName = TestCasesMap[SUTTestName];
           
            DotNetTest(s => s
                .SetProjectFile(sutTestProject)
                .SetLogger("console;verbosity=detailed")
                .SetFilter($"FullyQualifiedName={fullyQualifiedName}"));
        });
}