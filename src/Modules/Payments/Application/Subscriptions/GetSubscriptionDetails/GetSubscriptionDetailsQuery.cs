using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails
{
    public class GetSubscriptionDetailsQuery : QueryBase<SubscriptionDetailsDto>
    {
        public GetSubscriptionDetailsQuery(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }

        public Guid SubscriptionId { get; }
    }
}