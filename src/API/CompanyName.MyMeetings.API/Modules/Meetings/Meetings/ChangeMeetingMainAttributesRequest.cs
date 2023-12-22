namespace CompanyName.MyMeetings.API.Modules.Meetings.Meetings
{
    public class ChangeMeetingMainAttributesRequest
    {
        public Guid MeetingId { get; set; }

        public string Title { get; set; }

        public DateTime TermStartDate { get; set; }

        public DateTime TermEndDate { get; set; }

        public string Description { get; set; }

        public string MeetingLocationName { get; set; }

        public string MeetingLocationAddress { get; set; }

        public string MeetingLocationPostalCode { get; set; }

        public string MeetingLocationCity { get; set; }

        public int? AttendeesLimit { get; set; }

        public int GuestsLimit { get; set; }

        public DateTime? RSVPTermStartDate { get; set; }

        public DateTime? RSVPTermEndDate { get; set; }

        public decimal? EventFeeValue { get; set; }

        public string EventFeeCurrency { get; set; }

        public List<Guid> HostMemberIds { get; set; }
    }
}