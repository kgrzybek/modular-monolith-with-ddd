using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions.Events;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlStreamAggregateStore : IAggregateStore
    {
        private readonly string _connectionString;

        private readonly IDictionary<string, Type> _domainEventMappings;

        public SqlStreamAggregateStore(string connectionString)
        {
            _connectionString = connectionString;

            _domainEventMappings = new Dictionary<string, Type>();
            _domainEventMappings.Add("SubscriptionPurchased", typeof(SubscriptionPurchasedDomainEvent));
            _domainEventMappings.Add("SubscriptionRenewed", typeof(SubscriptionRenewedDomainEvent));
        }

        public async Task Save<T>(T aggregate) where T : AggregateRoot
        {
            var streamStore = GetSqlServerStore();

            var streamMessages = CreateStreamMessages(aggregate);
            
            await streamStore.AppendToStream(
                GetStreamId(aggregate), 
                aggregate.Version,
                streamMessages);
        }

        public async Task<T> Load<T>(AggregateId<T> aggregateId) where T : AggregateRoot
        {
            var streamStore = GetSqlServerStore();
            var streamId = GetStreamId(aggregateId);
            
            
            IList<IDomainEvent> domainEvents = new List<IDomainEvent>();
            ReadStreamPage readStreamPage;
            do
            {
                readStreamPage = await streamStore.ReadStreamForwards(streamId, StreamVersion.Start, maxCount: 100);
                var messages = readStreamPage.Messages;
                foreach (var streamMessage in messages)
                {
                    Type type = GetDomainEventType(streamMessage.Type);
                    var jsonData = await streamMessage.GetJsonData();
                    var domainEvent = JsonConvert.DeserializeObject(jsonData, type) as IDomainEvent;

                    domainEvents.Add(domainEvent);
                }
            } while (!readStreamPage.IsEnd);

            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            aggregate.Load(domainEvents);

            return aggregate;
        }

        private Type GetDomainEventType(string type)
        {
            return _domainEventMappings[type];
        }

        private NewStreamMessage[] CreateStreamMessages<T>(
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
                    MapDomainEventToType(domainEvent),
                    jsonData);
                newStreamMessages.Add(message);
            }

            return newStreamMessages.ToArray();
        }

        private string MapDomainEventToType(IDomainEvent domainEvent)
        {
            foreach (var key in _domainEventMappings.Keys)
            {
                if (_domainEventMappings[key] == domainEvent.GetType())
                {
                    return key;
                }
            }
            
            throw new ArgumentException("Invalid Domain Event type", nameof(domainEvent));
        }

        private static string GetStreamId<T>(T aggregate) where T : AggregateRoot
        {
            return $"{typeof(T).Name}-{aggregate.Id:N}";
        }

        private static string GetStreamId<T>(AggregateId<T> aggregateId)
            where T : AggregateRoot
            => $"{typeof(T).Name}-{aggregateId.Value:N}";

        private IStreamStore GetSqlServerStore()
        {
            return new MsSqlStreamStore(new MsSqlStreamStoreSettings(_connectionString));
        }
    }
}