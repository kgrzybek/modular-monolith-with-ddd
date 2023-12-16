using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.LeaveMeetingGroup
{
    public class LeaveMeetingGroupCommand : CommandBase
    {
        public LeaveMeetingGroupCommand(Guid meetingGroupId)
        {
            MeetingGroupId = meetingGroupId;
        }

        internal Guid MeetingGroupId { get; }
    }
}