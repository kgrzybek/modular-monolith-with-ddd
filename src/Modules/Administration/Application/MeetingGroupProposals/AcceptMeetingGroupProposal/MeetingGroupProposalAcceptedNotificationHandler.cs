using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using CompanyName.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    /// <summary>
    /// Handles the notification when a meeting group proposal is accepted.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MeetingGroupProposalAcceptedNotificationHandler"/> class.
    /// </remarks>
    /// <param name="eventsBus">The events bus.</param>
    public class MeetingGroupProposalAcceptedNotificationHandler(IEventsBus eventsBus) : INotificationHandler<MeetingGroupProposalAcceptedNotification>
    {
        private readonly IEventsBus _eventsBus = eventsBus;

        /// <summary>
        /// Handles the <see cref="MeetingGroupProposalAcceptedNotification"/> when a meeting group proposal is accepted.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
        {
            await _eventsBus.Publish(new MeetingGroupProposalAcceptedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.MeetingGroupProposalId.Value));
        }
    }
}