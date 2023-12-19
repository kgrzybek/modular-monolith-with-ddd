using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using Dapper;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus
{
    /// <summary>
    /// Represents a generic handler for an integration event.
    /// </summary>
    /// <typeparam name="T">The type of the integration event.</typeparam>
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        /// <summary>
        /// Handles an integration event, saving it to the inbox.
        /// </summary>
        /// <param name="event">The integration event to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(T @event)
        {
            using var scope = AdministrationCompositionRoot.BeginLifetimeScope();
            using var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection();

            string type = @event.GetType().FullName;
            var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var sql = "INSERT INTO [administration].[InboxMessages] (Id, OccurredOn, Type, Data) " +
                      "VALUES (@Id, @OccurredOn, @Type, @Data)";

            await connection.ExecuteScalarAsync(sql, new
            {
                @event.Id,
                @event.OccurredOn,
                type,
                data
            });
        }
    }
}