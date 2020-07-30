using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules
{
    public class MeetingGroupProposalRejectionMustHaveAReasonRule : IBusinessRule
    {
        private readonly string _reason;

        internal MeetingGroupProposalRejectionMustHaveAReasonRule(string reason)
        {
            _reason = reason;
        }

        public string Message => "Meeting group proposal rejection must have a reason";

        public bool IsBroken() => string.IsNullOrEmpty(_reason);
    }
}