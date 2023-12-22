using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup
{
    public class JoinToGroupCommand : CommandBase
    {
        public JoinToGroupCommand(Guid meetingGroupId)
        {
            MeetingGroupId = meetingGroupId;
        }

        internal Guid MeetingGroupId { get; }
    }
}