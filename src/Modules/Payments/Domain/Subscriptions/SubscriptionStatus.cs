using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriptionStatus : ValueObject
    {
        public static SubscriptionStatus Active => new SubscriptionStatus(nameof(Active));

        public static SubscriptionStatus Expired => new SubscriptionStatus(nameof(Expired));

        public string Code { get; }

        private SubscriptionStatus(string code)
        {
            Code = code;
        }
    }
}