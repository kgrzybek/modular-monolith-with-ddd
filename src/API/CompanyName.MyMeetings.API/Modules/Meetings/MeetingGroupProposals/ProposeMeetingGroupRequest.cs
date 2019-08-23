namespace CompanyName.MyMeetings.API.Modules.Meetings.MeetingGroupProposals
{
    public class ProposeMeetingGroupRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationCity { get; set; }

        public string LocationCountryCode { get; set; }
    }
}