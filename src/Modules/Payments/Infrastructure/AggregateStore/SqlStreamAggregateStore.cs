﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SqlStreamAggregateStore : IAggregateStore
    {
        private readonly IStreamStore _streamStore;

        private readonly List<IDomainEvent> _appendedChanges;

        private readonly List<AggregateToSave> _aggregatesToSave;

        public SqlStreamAggregateStore(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _appendedChanges = new List<IDomainEvent>();

            _streamStore =
                new MsSqlStreamStore(
                    new MsSqlStreamStoreSettings(sqlConnectionFactory.GetConnectionString())
                    {
                        Schema = DatabaseSchema.Name
                    });

            _aggregatesToSave = new List<AggregateToSave>();
        }

        public async Task Save()
        {
            foreach (var aggregateToSave in _aggregatesToSave)
            {
                await _streamStore.AppendToStream(
                    GetStreamId(aggregateToSave.Aggregate),
                    aggregateToSave.Aggregate.Version,
                    aggregateToSave.Messages.ToArray());
            }

            _aggregatesToSave.Clear();
        }

        public async Task<T> Load<T>(AggregateId<T> aggregateId) where T : AggregateRoot
        {
            var streamId = GetStreamId(aggregateId);

            IList<IDomainEvent> domainEvents = new List<IDomainEvent>();
            ReadStreamPage readStreamPage;
            do
            {
                readStreamPage = await _streamStore.ReadStreamForwards(streamId, StreamVersion.Start, maxCount: 100);
                var messages = readStreamPage.Messages;
                foreach (var streamMessage in messages)
                {
                    Type type = DomainEventTypeMappings.Dictionary[streamMessage.Type];
                    var jsonData = await streamMessage.GetJsonData();
                    var domainEvent = JsonConvert.DeserializeObject(jsonData, type) as IDomainEvent;

                    domainEvents.Add(domainEvent);
                }
            } while (!readStreamPage.IsEnd);

            if (!domainEvents.Any())
            {
                return null;
            }

            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            aggregate.Load(domainEvents);

            return aggregate;
        }

        public List<IDomainEvent> GetChanges()
        {
            return _appendedChanges;
        }

        public void AppendChanges<T>(T aggregate) where T : AggregateRoot
        {
            _aggregatesToSave.Add(new AggregateToSave(aggregate, CreateStreamMessages(aggregate).ToList()));
        }

        public void ClearChanges()
        {
            _appendedChanges.Clear();
        }

        private class AggregateToSave
        {
            public AggregateToSave(AggregateRoot aggregate, List<NewStreamMessage> messages)
            {
                Aggregate = aggregate;
                Messages = messages;
            }

            public AggregateRoot Aggregate { get; }

            public List<NewStreamMessage> Messages { get; }
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
                _appendedChanges.Add(domainEvent);
            }

            return newStreamMessages.ToArray();
        }

        private string MapDomainEventToType(IDomainEvent domainEvent)
        {
            foreach (var key in DomainEventTypeMappings.Dictionary.Keys)
            {
                if (DomainEventTypeMappings.Dictionary[key] == domainEvent.GetType())
                {
                    return key;
                }
            }

            throw new ArgumentException("Invalid Domain Event type", nameof(domainEvent));
        }

        private static string GetStreamId<T>(T aggregate) where T : AggregateRoot
        {
            return $"{aggregate.GetType().Name}-{aggregate.Id:N}";
        }

        private static string GetStreamId<T>(AggregateId<T> aggregateId)
            where T : AggregateRoot
            => $"{typeof(T).Name}-{aggregateId.Value:N}";
    }
}