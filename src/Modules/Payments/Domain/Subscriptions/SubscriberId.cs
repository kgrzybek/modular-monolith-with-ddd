using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriberId : TypedIdValueBase
    {
        public SubscriberId(Guid value)
            : base(value)
        {
        }
    }
}