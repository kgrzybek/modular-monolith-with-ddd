using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlStreamAggregateStore : IAggregateStore
    {
        private readonly string _connectionString;

        public SqlStreamAggregateStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot
        {
            var streamStore = GetSqlServerStore();

            var streamMessages = CreateStreamMessages(aggregate);
            
            await streamStore.AppendToStream(
                GetStreamId(aggregate), 
                aggregate.VersionId,
                streamMessages);
        }

        private static NewStreamMessage[] CreateStreamMessages<T>(
            T aggregate) where T : AggregateRoot
        {
            List<NewStreamMessage> newStreamMessages = new List<NewStreamMessage>();
            
            var domainEvents = aggregate.GetDomainEvents();
            
            foreach (var domainEvent in domainEvents)
            {
                string jsonData = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });
                var message = new NewStreamMessage(
                    domainEvent.Id,
                    MapAggregateRootToType(aggregate),
                    jsonData);
                newStreamMessages.Add(message);
            }

            return newStreamMessages.ToArray();
        }

        private static string MapAggregateRootToType<T>(T aggregate) where T : AggregateRoot
        {
            if (aggregate is Subscription)
            {
                return "Subscription";
            }

            throw new ArgumentException("Invalid Aggregate Root type", nameof(aggregate));
        }

        private static string GetStreamId<T>(T aggregate) where T : AggregateRoot
        {
            return $"{typeof(T).Name}-{aggregate.Id:N}";
        }

        private IStreamStore GetSqlServerStore()
        {
            return new MsSqlStreamStore(
                new MsSqlStreamStoreSettings(_connectionString));
        }
    }
}