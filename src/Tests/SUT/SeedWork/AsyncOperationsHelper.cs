using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    internal static class AsyncOperationsHelper
    {
        public static async Task WaitForProcessing(string connectionString, int timeoutInSeconds = 20)
        {
            await using var sqlConnection = new SqlConnection(connectionString);

            var start = Stopwatch.StartNew();
            while (start.Elapsed.Seconds < timeoutInSeconds)
            {
                var internalCommandsCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [users].[InternalCommands] AS [InternalCommand] 
                    WHERE [InternalCommand].[ProcessedDate] IS NULL
                    """);

                var inboxCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [users].[InboxMessages] AS [InboxMessage] 
                    WHERE [InboxMessage].[ProcessedDate] IS NULL
                    """);

                var outboxCountUsers = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [users].[OutboxMessages] AS [OutboxMessage] 
                    WHERE [OutboxMessage].[ProcessedDate] IS NULL
                    """);

                var internalCommandsCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [meetings].[InternalCommands] AS [InternalCommand] 
                    WHERE [InternalCommand].[ProcessedDate] IS NULL
                    """);

                var inboxCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [meetings].[InboxMessages] AS [InboxMessage] 
                    WHERE [InboxMessage].[ProcessedDate] IS NULL
                    """);

                var outboxCountMeetings = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [meetings].[OutboxMessages] AS [OutboxMessage] 
                    WHERE [OutboxMessage].[ProcessedDate] IS NULL
                    """);

                var internalCommandsCountAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [administration].[InternalCommands] AS [InternalCommand] 
                    WHERE [InternalCommand].[ProcessedDate] IS NULL
                    """);

                var inboxCountMeetingsAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [administration].[InboxMessages] AS [InboxMessage] 
                    WHERE [InboxMessage].[ProcessedDate] IS NULL
                    """);

                var outboxCountMeetingsAdministration = await sqlConnection.ExecuteScalarAsync<int>(
                    """
                    SELECT COUNT(*) 
                    FROM [administration].[OutboxMessages] AS [OutboxMessage] 
                    WHERE [OutboxMessage].[ProcessedDate] IS NULL
                    """);

                if (internalCommandsCountUsers == 0 &&
                    inboxCountUsers == 0 &&
                    outboxCountUsers == 0 &&
                    internalCommandsCountMeetings == 0 &&
                    inboxCountMeetings == 0 &&
                    outboxCountMeetings == 0 &&
                    internalCommandsCountAdministration == 0 &&
                    inboxCountMeetingsAdministration == 0 &&
                    outboxCountMeetingsAdministration == 0)
                {
                    return;
                }

                Thread.Sleep(100);
            }

            throw new Exception("Timeout for processing elapsed.");
        }
    }
}