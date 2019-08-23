using System;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventsBus : IDisposable
    {
        void Publish<T>(T @event) where T : IntegrationEvent;

        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent;

        void StartConsuming();
    }
}