using CompanyName.MyMeetings.BuildingBlocks.Domain;
namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalStatus : ValueObject
    {
        public static MeetingGroupProposalStatus ToVerify => new MeetingGroupProposalStatus("ToVerify");
        public static MeetingGroupProposalStatus Verified => new MeetingGroupProposalStatus("Verified");

        public string Value { get; }

        private MeetingGroupProposalStatus(string value)
        {
            Value = value;
        }

        internal static MeetingGroupProposalStatus Create(string value)
        {
            return new MeetingGroupProposalStatus(value);
        }
    }
}