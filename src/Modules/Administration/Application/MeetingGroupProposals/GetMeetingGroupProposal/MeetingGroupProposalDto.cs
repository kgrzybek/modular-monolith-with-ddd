namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal
{
    /// <summary>
    /// Represents a data transfer object for a meeting group proposal.
    /// </summary>
    public class MeetingGroupProposalDto
    {
        /// <summary>
        /// Gets or sets the ID of the meeting group proposal.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the meeting group proposal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the meeting group proposal.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the city of the meeting group proposal location.
        /// </summary>
        public string LocationCity { get; set; }

        /// <summary>
        /// Gets or sets the country code of the meeting group proposal location.
        /// </summary>
        public string LocationCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who proposed the meeting group.
        /// </summary>
        public Guid ProposalUserId { get; set; }

        /// <summary>
        /// Gets or sets the date when the meeting group proposal was made.
        /// </summary>
        public DateTime ProposalDate { get; set; }

        /// <summary>
        /// Gets or sets the status code of the meeting group proposal.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the date when the decision was made for the meeting group proposal.
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who made the decision for the meeting group proposal.
        /// </summary>
        public Guid? DecisionUserId { get; set; }

        /// <summary>
        /// Gets or sets the decision code of the meeting group proposal.
        /// </summary>
        public string DecisionCode { get; set; }

        /// <summary>
        /// Gets or sets the reason for rejecting the meeting group proposal.
        /// </summary>
        public string DecisionRejectReason { get; set; }
    }
}