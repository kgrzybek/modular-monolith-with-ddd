using CompanyName.MyMeetings.BuildingBlocks.Application.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals
{
    public class MeetingGroupProposedNotification : DomainNotificationBase<MeetingGroupProposedDomainEvent>
    {
        [JsonConstructor]
        public MeetingGroupProposedNotification(MeetingGroupProposedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}