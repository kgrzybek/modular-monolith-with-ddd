using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole
{
    public class SetMeetingHostRoleCommand : CommandBase
    {
        public Guid MemberId { get; }

        public Guid MeetingId { get; }

        public SetMeetingHostRoleCommand(Guid memberId, Guid meetingId)
        {
            MemberId = memberId;
            MeetingId = meetingId;
        }
    }
}