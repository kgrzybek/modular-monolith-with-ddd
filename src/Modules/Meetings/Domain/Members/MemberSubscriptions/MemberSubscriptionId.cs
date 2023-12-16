using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions
{
    public class MemberSubscriptionId : TypedIdValueBase
    {
        public MemberSubscriptionId(Guid value)
            : base(value)
        {
        }
    }
}