using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalStatus : ValueObject
    {
        internal static MeetingGroupProposalStatus InVerification => new MeetingGroupProposalStatus("InVerification");
        internal static MeetingGroupProposalStatus Accepted => new MeetingGroupProposalStatus("Accepted");
        
        public string Value { get; }

        private MeetingGroupProposalStatus(string value)
        {
            Value = value;
        }
    }
}