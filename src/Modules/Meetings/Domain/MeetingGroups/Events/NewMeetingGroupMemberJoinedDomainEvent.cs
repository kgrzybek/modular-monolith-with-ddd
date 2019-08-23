using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events
{
    public class NewMeetingGroupMemberJoinedDomainEvent : DomainEventBase
    {
        public MeetingGroupId MeetingGroupId { get; }

        public MemberId MemberId { get; }

        public MeetingGroupMemberRole Role { get; }

        public NewMeetingGroupMemberJoinedDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId, MeetingGroupMemberRole role)
        {
            this.MeetingGroupId = meetingGroupId;
            this.MemberId = memberId;
            this.Role = role;
        }
    }
}
