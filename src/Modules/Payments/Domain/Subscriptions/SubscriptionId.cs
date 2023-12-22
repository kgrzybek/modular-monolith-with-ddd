using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriptionId : AggregateId<Subscription>
    {
        public SubscriptionId(Guid value)
            : base(value)
        {
        }
    }
}