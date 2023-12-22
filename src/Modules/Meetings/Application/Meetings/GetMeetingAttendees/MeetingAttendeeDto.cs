namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees
{
    public class MeetingAttendeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid AttendeeId { get; set; }

        public string RoleCode { get; set; }

        public int GuestsNumber { get; set; }

        public DateTime DecisionDate { get; set; }
    }
}