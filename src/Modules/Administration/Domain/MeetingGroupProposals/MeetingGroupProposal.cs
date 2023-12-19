using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents a meeting group proposal entity.
    /// </summary>
    public class MeetingGroupProposal : Entity, IAggregateRoot
    {
        private string _name;

        private string _description;

        private MeetingGroupLocation _location;

        private DateTime _proposalDate;

        private UserId _proposalUserId;

        private MeetingGroupProposalStatus _status;

        private MeetingGroupProposalDecision _decision;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupProposal"/> class.
        /// </summary>
        /// <param name="id">The ID of the meeting group proposal.</param>
        /// <param name="name">The name of the meeting group proposal.</param>
        /// <param name="description">The description of the meeting group proposal.</param>
        /// <param name="location">The location of the meeting group proposal.</param>
        /// <param name="proposalUserId">The user ID of the proposal creator.</param>
        /// <param name="proposalDate">The date of the proposal.</param>
        private MeetingGroupProposal(
            MeetingGroupProposalId id,
            string name,
            string description,
            MeetingGroupLocation location,
            UserId proposalUserId,
            DateTime proposalDate)
        {
            Id = id;
            _name = name;
            _description = description;
            _location = location;
            _proposalUserId = proposalUserId;
            _proposalDate = proposalDate;

            _status = MeetingGroupProposalStatus.ToVerify;
            _decision = MeetingGroupProposalDecision.NoDecision;

            this.AddDomainEvent(new MeetingGroupProposalVerificationRequestedDomainEvent(this.Id));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingGroupProposal"/> class.
        /// </summary>
        private MeetingGroupProposal()
        {
            _decision = MeetingGroupProposalDecision.NoDecision;
        }

        /// <summary>
        /// Gets the identifier of the meeting group proposal.
        /// </summary>
        public MeetingGroupProposalId Id { get; private set; }

        /// <summary>
        /// Accepts the meeting group proposal.
        /// </summary>
        /// <param name="userId">The ID of the user accepting the proposal.</param>
        public void Accept(UserId userId)
        {
            this.CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));

            _decision = MeetingGroupProposalDecision.AcceptDecision(DateTime.UtcNow, userId);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(this.Id));
        }

        /// <summary>
        /// Rejects the meeting group proposal.
        /// </summary>
        /// <param name="userId">The ID of the user rejecting the proposal.</param>
        /// <param name="rejectReason">The reason for rejecting the proposal.</param>
        public void Reject(UserId userId, string rejectReason)
        {
            this.CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));
            this.CheckRule(new MeetingGroupProposalRejectionMustHaveAReasonRule(rejectReason));

            _decision = MeetingGroupProposalDecision.RejectDecision(DateTime.UtcNow, userId, rejectReason);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalRejectedDomainEvent(this.Id));
        }

        /// <summary>
        /// Represents a meeting group proposal.
        /// </summary>
        /// <param name="meetingGroupProposalId">The ID of the meeting group proposal.</param>
        /// <param name="name">The name of the meeting group proposal.</param>
        /// <param name="description">The description of the meeting group proposal.</param>
        /// <param name="location">The location of the meeting group proposal.</param>
        /// <param name="proposalUserId">The user ID of the proposal creator.</param>
        /// <param name="proposalDate">The date of the proposal.</param>
        /// <returns>A new instance of the <see cref="MeetingGroupProposal"/> class.</returns>
        public static MeetingGroupProposal CreateToVerify(
            Guid meetingGroupProposalId,
            string name,
            string description,
            MeetingGroupLocation location,
            UserId proposalUserId,
            DateTime proposalDate)
        {
            var meetingGroupProposal = new MeetingGroupProposal(
                new MeetingGroupProposalId(meetingGroupProposalId),
                name,
                description,
                location,
                proposalUserId,
                proposalDate);

            return meetingGroupProposal;
        }
    }
}
