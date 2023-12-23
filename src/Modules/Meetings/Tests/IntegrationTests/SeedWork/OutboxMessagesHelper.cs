using System.Data;
using System.Reflection;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = $"""
                                SELECT [OutboxMessage].[Id] as [{nameof(OutboxMessageDto.Id)}], 
                                       [OutboxMessage].[Type] as [{nameof(OutboxMessageDto.Type)}],
                                       [OutboxMessage].[Data] as [{nameof(OutboxMessageDto.Data)}] 
                                FROM [meetings].[OutboxMessages] AS [OutboxMessage] 
                                ORDER BY [OutboxMessage].[OccurredOn]
                                """;

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            return messages.AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            Type type = Assembly.GetAssembly(typeof(MeetingGroupProposalAcceptedNotification)).GetType(message.Type);
            return JsonConvert.DeserializeObject(message.Data, type) as T;
        }
    }
}