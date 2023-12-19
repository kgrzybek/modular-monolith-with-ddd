using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals
{
    /// <summary>
    /// Handles the integration event when a meeting group is proposed.
    /// </summary>
    internal class MeetingGroupProposedIntegrationEventHandler : INotificationHandler<MeetingGroupProposedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupProposedIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="commandsScheduler">The commands scheduler.</param>
        internal MeetingGroupProposedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        /// <summary>
        /// Handles the meeting group proposed integration event.
        /// </summary>
        /// <param name="notification">The integration event notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(MeetingGroupProposedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new RequestMeetingGroupProposalVerificationCommand(
                    Guid.NewGuid(),
                    notification.MeetingGroupProposalId,
                    notification.Name,
                    notification.Description,
                    notification.LocationCity,
                    notification.LocationCountryCode,
                    notification.ProposalUserId,
                    notification.ProposalDate));
        }
    }
}