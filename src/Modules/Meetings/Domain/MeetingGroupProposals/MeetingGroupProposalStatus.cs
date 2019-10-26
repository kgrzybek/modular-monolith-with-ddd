using System.Reflection.Metadata;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalStatus : ValueObject
    {
        internal bool IsAccepted => Value == "Accepted";
        internal static MeetingGroupProposalStatus CreateInVerification => new MeetingGroupProposalStatus("InVerification");
        internal static MeetingGroupProposalStatus CreateAccepted => new MeetingGroupProposalStatus("Accepted");
        
        public string Value { get; }

        private MeetingGroupProposalStatus(string value)
        {
            Value = value;
        }
    }
}