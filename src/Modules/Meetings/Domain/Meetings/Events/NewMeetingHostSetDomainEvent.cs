using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events
{
    public class NewMeetingHostSetDomainEvent : DomainEventBase
    {
        public NewMeetingHostSetDomainEvent(MeetingId meetingId, MemberId hostId)
        {
            MeetingId = meetingId;
            HostId = hostId;
        }

        public MeetingId MeetingId { get; }

        public MemberId HostId { get; }
    }
}