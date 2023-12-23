using System.Data;
using System.Reflection;
using CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.CreateSubscription;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing.Outbox;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = """
                               SELECT
                                   [OutboxMessage].[Id], 
                                   [OutboxMessage].[Type],
                                   [OutboxMessage].[Data] 
                               FROM [payments].[OutboxMessages] AS [OutboxMessage] 
                               ORDER BY [OutboxMessage].[OccurredOn]
                               """;

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(SubscriptionCreatedNotification)).GetType(message.Type);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }
    }
}