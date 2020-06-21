namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies
{
    public class MeetingGroupMemberData
    {
        public MeetingGroupMemberData(MeetingGroupId meetingGroupId, MeetingGroupMemberRole role)
        {
            MeetingGroupId = meetingGroupId;
            Role = role;
        }

        public MeetingGroupId MeetingGroupId { get; }

        public MeetingGroupMemberRole Role { get; }
    }
}