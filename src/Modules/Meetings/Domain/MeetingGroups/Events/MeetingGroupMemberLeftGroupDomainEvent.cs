using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class MeetingGroupMemberLeftGroupDomainEvent : DomainEventBase
    {
        public MeetingGroupMemberLeftGroupDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId)
        {
            MeetingGroupId = meetingGroupId;
            MemberId = memberId;
        }

        public MeetingGroupId MeetingGroupId { get; }

        public MemberId MemberId { get; }
    }
}