using Autofac;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public class SubscriptionsManager
    {
        private readonly IStreamStore _streamStore;

        public SubscriptionsManager(
            IStreamStore streamStore)
        {
            _streamStore = streamStore;
        }

        public void Start()
        {
            long? actualPosition;

            using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
            {
                var checkpointStore = scope.Resolve<ICheckpointStore>();

                actualPosition = checkpointStore.GetCheckpoint(SubscriptionCode.All);
            }

            _streamStore.SubscribeToAll(actualPosition, StreamMessageReceived);
        }

        public void Stop()
        {
            _streamStore.Dispose();
        }

        private static async Task StreamMessageReceived(
            IAllStreamSubscription subscription, StreamMessage streamMessage, CancellationToken cancellationToken)
        {
            var type = DomainEventTypeMappings.Dictionary[streamMessage.Type];
            var jsonData = await streamMessage.GetJsonData(cancellationToken);
            var domainEvent = JsonConvert.DeserializeObject(jsonData, type) as IDomainEvent;

            using var scope = PaymentsCompositionRoot.BeginLifetimeScope();

            var projectors = scope.Resolve<IList<IProjector>>();

            foreach (var projector in projectors)
            {
                await projector.Project(domainEvent);
            }

            var checkpointStore = scope.Resolve<ICheckpointStore>();
            await checkpointStore.StoreCheckpoint(SubscriptionCode.All, streamMessage.Position);
        }
    }
}