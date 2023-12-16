using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.ChangeNotAttendeeDecision
{
    public class ChangeNotAttendeeDecisionCommand : CommandBase
    {
        public Guid MeetingId { get; }

        public ChangeNotAttendeeDecisionCommand(Guid meetingId)
        {
            MeetingId = meetingId;
        }
    }
}