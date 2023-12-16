using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    public class MeetingGroupProposalAcceptedNotification : DomainNotificationBase<MeetingGroupProposalAcceptedDomainEvent>
    {
        [JsonConstructor]
        public MeetingGroupProposalAcceptedNotification(MeetingGroupProposalAcceptedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}