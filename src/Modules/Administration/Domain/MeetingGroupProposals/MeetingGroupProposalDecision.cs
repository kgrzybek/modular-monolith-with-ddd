using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents a decision made on a meeting group proposal.
    /// </summary>
    public class MeetingGroupProposalDecision : ValueObject
    {
        private MeetingGroupProposalDecision(DateTime? date, UserId userId, string code, string rejectReason)
        {
            this.Date = date;
            this.UserId = userId;
            this.Code = code;
            this.RejectReason = rejectReason;
        }

        /// <summary>
        /// Gets the date associated with the meeting group proposal decision.
        /// </summary>
        public DateTime? Date { get; }

        /// <summary>
        /// Gets the user ID associated with the meeting group proposal decision.
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets the code of the meeting group proposal decision.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the reason for rejecting the meeting group proposal.
        /// </summary>
        public string RejectReason { get; }

        internal static MeetingGroupProposalDecision NoDecision =>
            new MeetingGroupProposalDecision(null, null, null, null);

        private bool IsAccepted => this.Code == "Accept";

        private bool IsRejected => this.Code == "Reject";

        internal static MeetingGroupProposalDecision AcceptDecision(DateTime date, UserId userId)
        {
            return new MeetingGroupProposalDecision(date, userId, "Accept", null);
        }

        internal static MeetingGroupProposalDecision RejectDecision(DateTime date, UserId userId, string rejectReason)
        {
            return new MeetingGroupProposalDecision(date, userId, "Reject", rejectReason);
        }

        internal MeetingGroupProposalStatus GetStatusForDecision()
        {
            if (this.IsAccepted)
            {
                return MeetingGroupProposalStatus.Verified;
            }

            if (this.IsRejected)
            {
                return MeetingGroupProposalStatus.Create("Rejected");
            }

            return MeetingGroupProposalStatus.ToVerify;
        }
    }
}