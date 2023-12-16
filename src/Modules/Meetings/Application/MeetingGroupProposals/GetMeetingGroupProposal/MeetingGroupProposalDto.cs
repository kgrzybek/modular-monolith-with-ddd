namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    public class MeetingGroupProposalDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationCity { get; set; }

        public string LocationCountryCode { get; set; }

        public Guid ProposalUserId { get; set; }

        public DateTime ProposalDate { get; set; }

        public string StatusCode { get; set; }
    }
}