using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee
{
    public class RemoveMeetingAttendeeCommand : CommandBase
    {
        public RemoveMeetingAttendeeCommand(Guid meetingId, Guid attendeeId, string removingReason)
        {
            MeetingId = meetingId;
            AttendeeId = attendeeId;
            RemovingReason = removingReason;
        }

        public Guid MeetingId { get; }

        public Guid AttendeeId { get; }

        public string RemovingReason { get; }
    }
}