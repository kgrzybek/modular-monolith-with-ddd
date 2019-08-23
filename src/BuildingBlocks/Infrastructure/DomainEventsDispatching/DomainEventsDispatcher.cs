using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.SeedWork;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly ILifetimeScope _scope;
        private readonly IOutbox _outbox;
        private readonly IDomainEventsAccessor _domainEventsProvider;

        public DomainEventsDispatcher(
            IMediator mediator, 
            ILifetimeScope scope, 
            IOutbox outbox,
            IDomainEventsAccessor domainEventsProvider)
        {
            this._mediator = mediator;
            this._scope = scope;
            this._outbox = outbox;
            _domainEventsProvider = domainEventsProvider;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();


            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as SeedWork.IDomainEventNotification<IDomainEvent>);
                }
            }

            _domainEventsProvider.ClearAllDomainEvents();

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                _outbox.Add(outboxMessage);
            }
        }

        private class AllPropertiesContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Select(p => base.CreateProperty(p, memberSerialization))
                    .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Select(f => base.CreateProperty(f, memberSerialization)))
                    .ToList();
                props.ForEach(p =>
                {
                    p.Writable = true;
                    p.Readable = true;
                });
                return props;
            }
        }
    }
}