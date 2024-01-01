using System.Data;
using System.Reflection;
using CompanyName.MyMeetings.Modules.Registrations.Application.Contracts;
using CompanyName.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.Outbox;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyNames.MyMeetings.Modules.Registrations.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = $"""
                               SELECT 
                                   [OutboxMessage].[Id] as [{nameof(OutboxMessageDto.Id)}], 
                                   [OutboxMessage].[Type] as [{nameof(OutboxMessageDto.Type)}], 
                                   [OutboxMessage].[Data] as [{nameof(OutboxMessageDto.Data)}] 
                               FROM [registrations].[OutboxMessages] AS [OutboxMessage] 
                               ORDER BY [OutboxMessage].[OccurredOn]
                               """;

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(CommandBase)).GetType(typeof(T).FullName);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }
    }
}