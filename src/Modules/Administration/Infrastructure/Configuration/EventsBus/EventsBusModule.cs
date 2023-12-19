using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus
{
    /// <summary>
    /// Represents a module for configuring the events bus.
    /// </summary>
    internal class EventsBusModule : Autofac.Module
    {
        private readonly IEventsBus _eventsBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsBusModule"/> class.
        /// </summary>
        /// <param name="eventsBus">The events bus.</param>
        public EventsBusModule(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_eventsBus != null)
            {
                builder.RegisterInstance(_eventsBus).SingleInstance();
            }
            else
            {
                builder.RegisterType<InMemoryEventBusClient>()
                    .As<IEventsBus>()
                    .SingleInstance();
            }
        }
    }
}