using System;
using System.Threading;
using Nuke.Common;
using Dapper;
using System.Data.SqlClient;

namespace Utils
{
    public static class SqlReadinessChecker
    {
        public static void WaitForSqlSever(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);

            const int maxTryCounts = 30;
            var tryCounts = 0;
            while (true)
            {
                tryCounts++;
                try
                {
                    connection.QuerySingle<string>("SELECT @@Version");
                    Logger.Info("Sql Server started");
                    break;
                }
                catch
                {
                    Logger.Info("Sql Server not ready");
                    if (tryCounts > maxTryCounts)
                    {
                        throw new Exception("Sql Server cannot start.");
                    }
                }

                Thread.Sleep(2000);
            }
        }
    }
}