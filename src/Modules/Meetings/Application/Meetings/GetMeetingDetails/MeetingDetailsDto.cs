namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails
{
    public class MeetingDetailsDto
    {
        public Guid Id { get; set; }

        public Guid MeetingGroupId { get; set; }

        public string Title { get; set; }

        public DateTime TermStartDate { get; set; }

        public DateTime TermEndDate { get; set; }

        public string Description { get; set; }

        public string LocationName { get; set; }

        public string LocationAddress { get; set; }

        public string LocationPostalCode { get; set; }

        public string LocationCity { get; set; }

        public int? AttendeesLimit { get; set; }

        public int GuestsLimit { get; set; }

        public DateTime? RSVPTermStartDate { get; set; }

        public DateTime? RSVPTermEndDate { get; set; }

        public decimal? EventFeeValue { get; set; }

        public string EventFeeCurrency { get; set; }
    }
}