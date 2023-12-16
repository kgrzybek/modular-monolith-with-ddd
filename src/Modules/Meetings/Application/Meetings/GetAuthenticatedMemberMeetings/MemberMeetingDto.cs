namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetAuthenticatedMemberMeetings
{
    public class MemberMeetingDto
    {
        public Guid MeetingId { get; set; }

        public string Title { get; set; }

        public string LocationAddress { get; set; }

        public string LocationCity { get; set; }

        public string LocationPostalCode { get; set; }

        public DateTime TermStartDate { get; set; }

        public DateTime TermEndDate { get; set; }

        public string RoleCode { get; set; }
    }
}