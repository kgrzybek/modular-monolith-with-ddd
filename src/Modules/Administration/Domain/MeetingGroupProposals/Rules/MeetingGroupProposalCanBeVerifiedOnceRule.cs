using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules
{
    /// <summary>
    /// Represents a business rule that checks if a meeting group proposal can be verified only once.
    /// </summary>
    public class MeetingGroupProposalCanBeVerifiedOnceRule : IBusinessRule
    {
        private readonly MeetingGroupProposalDecision _actualDecision;

        internal MeetingGroupProposalCanBeVerifiedOnceRule(MeetingGroupProposalDecision actualDecision)
        {
            _actualDecision = actualDecision;
        }

        /// <summary>
        /// Gets the error message associated with the rule.
        /// </summary>
        public string Message => "Meeting group proposal can be verified only once";

        /// <summary>
        /// Checks if the rule is broken.
        /// </summary>
        /// <returns>True if the rule is broken, otherwise false.</returns>
        public bool IsBroken() => _actualDecision != MeetingGroupProposalDecision.NoDecision;
    }
}