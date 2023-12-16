using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingNotAttendee
{
    public class AddMeetingNotAttendeeCommand : CommandBase
    {
        public Guid MeetingId { get; }

        public AddMeetingNotAttendeeCommand(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}