﻿using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.BuildingBlocks.Application.Outbox;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Outbox;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox
{
    /// <summary>
    /// Represents a module responsible for configuring the outbox functionality.
    /// </summary>
    internal class OutboxModule : Autofac.Module
    {
        private readonly BiDictionary<string, Type> _domainNotificationsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxModule"/> class.
        /// </summary>
        /// <param name="domainNotificationsMap">The domain notifications map.</param>
        public OutboxModule(BiDictionary<string, Type> domainNotificationsMap)
        {
            _domainNotificationsMap = domainNotificationsMap;
        }

        /// <summary>
        /// Loads the outbox module into the Autofac container,
        /// registering all implementations of the <see cref="IOutbox"/> interface.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .FindConstructorsWith(new AllConstructorFinder())
                .InstancePerLifetimeScope();

            this.CheckMappings();

            builder.RegisterType<DomainNotificationsMapper>()
                .As<IDomainNotificationsMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("domainNotificationsMap", _domainNotificationsMap)
                .SingleInstance();
        }

        private void CheckMappings()
        {
            var domainEventNotifications = Assemblies.Application
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)))
                .ToList();

            List<Type> notMappedNotifications = new List<Type>();
            foreach (var domainEventNotification in domainEventNotifications)
            {
                _domainNotificationsMap.TryGetBySecond(domainEventNotification, out var name);

                if (name == null)
                {
                    notMappedNotifications.Add(domainEventNotification);
                }
            }

            if (notMappedNotifications.Any())
            {
                throw new ApplicationException($"Domain Event Notifications {notMappedNotifications.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
            }
        }
    }
}