using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposalId : TypedIdValueBase
    {
        public MeetingGroupProposalId(Guid value)
            : base(value)
        {
        }
    }
}