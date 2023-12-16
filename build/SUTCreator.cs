using System;
using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;


public partial class Build
{
    [Parameter("SUT creator test name to execute")] readonly string SUTTestName;

    readonly IDictionary<string, string> TestCasesMap = new Dictionary<string, string>
    {
        {"CleanDatabase", "CompanyName.MyMeetings.SUT.TestCases.CleanDatabaseTestCase.Prepare"},
        {"OnlyAdmin", "CompanyName.MyMeetings.SUT.TestCases.OnlyAdminTestCase.Prepare"},
        {"CreateMeeting", "CompanyName.MyMeetings.SUT.TestCases.CreateMeeting.Prepare"}
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
                
                .SetFilter($"FullyQualifiedName={fullyQualifiedName}"));
        });
}