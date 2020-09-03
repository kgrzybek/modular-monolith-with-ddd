using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;
using CompanyName.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.MarkMeetingFeeAsPaid
{
    public class MeetingFeePaidNotificationHandler : INotificationHandler<MeetingFeePaidNotification>
    {
        private readonly IEventsBus _eventsBus;

        private readonly IAggregateStore _aggregateStore;

        public MeetingFeePaidNotificationHandler(IEventsBus eventsBus, IAggregateStore aggregateStore)
        {
            _eventsBus = eventsBus;
            _aggregateStore = aggregateStore;
        }

        public async Task Handle(MeetingFeePaidNotification notification, CancellationToken cancellationToken)
        {
            var meetingFee = await _aggregateStore.Load(new MeetingFeeId(notification.DomainEvent.MeetingFeeId));

            var meetingFeeSnapshot = meetingFee.GetSnapshot();

            _eventsBus.Publish(new MeetingFeePaidIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                meetingFeeSnapshot.PayerId,
                meetingFeeSnapshot.MeetingId));
        }
    }
}