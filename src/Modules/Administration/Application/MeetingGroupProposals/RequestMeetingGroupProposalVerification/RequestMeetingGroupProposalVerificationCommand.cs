using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification
{
    /// <summary>
    /// Represents a command to request verification for a meeting group proposal.
    /// </summary>
    public class RequestMeetingGroupProposalVerificationCommand : InternalCommandBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMeetingGroupProposalVerificationCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the command.</param>
        /// <param name="meetingGroupProposalId">The unique identifier of the meeting group proposal.</param>
        /// <param name="name">The name of the meeting group proposal.</param>
        /// <param name="description">The description of the meeting group proposal.</param>
        /// <param name="locationCity">The city of the meeting group proposal location.</param>
        /// <param name="locationCountryCode">The country code of the meeting group proposal location.</param>
        /// <param name="proposalUserId">The unique identifier of the user who proposed the meeting group.</param>
        /// <param name="proposalDate">The date when the meeting group proposal was made.</param>
        [JsonConstructor]
        public RequestMeetingGroupProposalVerificationCommand(
            Guid id,
            Guid meetingGroupProposalId,
            string name,
            string description,
            string locationCity,
            string locationCountryCode,
            Guid proposalUserId,
            DateTime proposalDate)
            : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
            this.Name = name;
            this.Description = description;
            this.LocationCity = locationCity;
            this.LocationCountryCode = locationCountryCode;
            this.ProposalUserId = proposalUserId;
            this.ProposalDate = proposalDate;
        }

        /// <summary>
        /// Gets the ID of the meeting group proposal.
        /// </summary>
        public Guid MeetingGroupProposalId { get; }

        /// <summary>
        /// Gets the name of the meeting group proposal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the meeting group proposal.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the city of the meeting group proposal location.
        /// </summary>
        public string LocationCity { get; }

        /// <summary>
        /// Gets the country code of the meeting group proposal location.
        /// </summary>
        public string LocationCountryCode { get; }

        /// <summary>
        /// Gets the ID of the user who proposed the meeting group.
        /// </summary>
        public Guid ProposalUserId { get; }

        /// <summary>
        /// Gets the date when the meeting group proposal was made.
        /// </summary>
        public DateTime ProposalDate { get; }
    }
}