using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlServerCheckpointStore : ICheckpointStore
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SqlServerCheckpointStore(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public long? GetCheckpoint(SubscriptionCode subscriptionCode)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = """
                               SELECT [SubscriptionCheckpoint].Position 
                               FROM [payments].[SubscriptionCheckpoints] AS [SubscriptionCheckpoint] 
                               WHERE [Code] = @Code
                               """;

            var checkpoint = connection.QuerySingleOrDefault<long?>(
                sql,
                new
                {
                    Code = subscriptionCode
                });

            return checkpoint;
        }

        public async Task StoreCheckpoint(SubscriptionCode subscriptionCode, long checkpoint)
        {
            var actualCheckpoint = GetCheckpoint(subscriptionCode);

            using var connection = _sqlConnectionFactory.GetOpenConnection();
            if (actualCheckpoint == null)
            {
                await connection.ExecuteScalarAsync(
                    "INSERT INTO [payments].[SubscriptionCheckpoints] VALUES (@Code, @Position)",
                    new
                    {
                        Code = subscriptionCode,
                        Position = checkpoint
                    });
            }
            else
            {
                await connection.ExecuteScalarAsync(
                    "UPDATE [payments].[SubscriptionCheckpoints] " +
                    "SET Position = @Position " +
                    "WHERE Code = @Code",
                    new
                    {
                        Code = subscriptionCode,
                        Position = checkpoint
                    });
            }
        }
    }
}