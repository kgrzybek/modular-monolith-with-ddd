using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents the status of a meeting group proposal.
    /// </summary>
    public class MeetingGroupProposalStatus : ValueObject
    {
        private MeetingGroupProposalStatus(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the "ToVerify" status.
        /// </summary>
        public static MeetingGroupProposalStatus ToVerify => new MeetingGroupProposalStatus("ToVerify");

        /// <summary>
        /// Gets the "Verified" status.
        /// </summary>
        public static MeetingGroupProposalStatus Verified => new MeetingGroupProposalStatus("Verified");

        /// <summary>
        /// Gets the value of the meeting group proposal status.
        /// </summary>
        public string Value { get; }

        internal static MeetingGroupProposalStatus Create(string value)
        {
            return new MeetingGroupProposalStatus(value);
        }
    }
}