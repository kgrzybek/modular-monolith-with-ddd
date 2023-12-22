namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings
{
    public class MeetingDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LocationAddress { get; set; }

        public string LocationCity { get; set; }

        public string LocationPostalCode { get; set; }

        public DateTime TermStartDate { get; set; }

        public DateTime TermEndDate { get; set; }
    }
}