using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    /// <summary>
    /// Represents a notification that is sent when a meeting group proposal is accepted.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MeetingGroupProposalAcceptedNotification"/> class.
    /// </remarks>
    /// <param name="domainEvent">The domain event associated with the notification.</param>
    /// <param name="id">The ID of the notification.</param>
    [method: JsonConstructor]
    public class MeetingGroupProposalAcceptedNotification(
        MeetingGroupProposalAcceptedDomainEvent domainEvent,
        Guid id) : DomainNotificationBase<MeetingGroupProposalAcceptedDomainEvent>(domainEvent, id)
    {
    }
}