using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole
{
    public class SetMeetingAttendeeRoleCommand : CommandBase
    {
        public Guid MemberId { get; }

        public Guid MeetingId { get; }

        public SetMeetingAttendeeRoleCommand(Guid memberId, Guid meetingId)
        {
            MemberId = memberId;
            MeetingId = meetingId;
        }
    }
}