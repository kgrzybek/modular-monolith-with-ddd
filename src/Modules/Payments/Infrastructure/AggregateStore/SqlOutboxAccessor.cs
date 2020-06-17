using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlOutboxAccessor : IOutbox
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SqlOutboxAccessor(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public void Add(OutboxMessage message)
        {
            const string sql = "INSERT INTO [payments].[OutboxMessages] " +
                               "([Id], [OccurredOn], [Type], [Data]) VALUES " +
                               "(@Id, @OccurredOn, @Type, @Data)";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            connection.ExecuteScalar(sql, new
            {
                message.Id,
                message.OccurredOn,
                message.Type,
                message.Data
            });
        }
    }
}