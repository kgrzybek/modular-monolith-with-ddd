﻿using Autofac;
using Autofac.Core;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Serialization;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Represents a class responsible for dispatching domain events.
    /// </summary>
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        private readonly ILifetimeScope _scope;

        private readonly IOutbox _outbox;

        private readonly IDomainEventsAccessor _domainEventsProvider;

        private readonly IDomainNotificationsMapper _domainNotificationsMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventsDispatcher"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="outbox">The outbox.</param>
        /// <param name="domainEventsProvider">The domain events provider.</param>
        /// <param name="domainNotificationsMapper">The domain notifications mapper.</param>
        public DomainEventsDispatcher(
            IMediator mediator,
            ILifetimeScope scope,
            IOutbox outbox,
            IDomainEventsAccessor domainEventsProvider,
            IDomainNotificationsMapper domainNotificationsMapper)
        {
            _mediator = mediator;
            _scope = scope;
            _outbox = outbox;
            _domainEventsProvider = domainEventsProvider;
            _domainNotificationsMapper = domainNotificationsMapper;
        }

        /// <summary>
        /// Dispatches the domain events asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", domainEvent.Id)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            _domainEventsProvider.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = _domainNotificationsMapper.GetName(domainEventNotification.GetType());
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);

                _outbox.Add(outboxMessage);
            }
        }
    }
}