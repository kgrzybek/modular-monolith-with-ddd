using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupMemberLeavedDomainEvent : DomainEventBase
    {
        public MeetingGroupMemberLeavedDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId)
        {
            MeetingGroupId = meetingGroupId;
            MemberId = memberId;
        }

        public MeetingGroupId MeetingGroupId { get; }
        public MemberId MemberId { get; }
    }
}