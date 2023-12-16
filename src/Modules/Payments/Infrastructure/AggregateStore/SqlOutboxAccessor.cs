using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlOutboxAccessor : IOutbox
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly List<OutboxMessage> _messages;

        public SqlOutboxAccessor(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _messages = [];
        }

        public void Add(OutboxMessage message)
        {
            _messages.Add(message);
        }

        public async Task Save()
        {
            if (_messages.Any())
            {
                using var connection = _sqlConnectionFactory.CreateNewConnection();

                const string sql = "INSERT INTO [payments].[OutboxMessages] " +
                                   "([Id], [OccurredOn], [Type], [Data]) VALUES " +
                                   "(@Id, @OccurredOn, @Type, @Data)";

                foreach (var message in _messages)
                {
                    await connection.ExecuteScalarAsync(sql, new
                    {
                        message.Id,
                        message.OccurredOn,
                        message.Type,
                        message.Data
                    });
                }

                _messages.Clear();
            }
        }
    }
}