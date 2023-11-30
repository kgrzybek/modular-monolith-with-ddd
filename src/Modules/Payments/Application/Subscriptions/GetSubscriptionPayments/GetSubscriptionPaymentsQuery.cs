using CompanyName.MyMeetings.BuildingBlocks.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments
{
    public class GetSubscriptionPaymentsQuery : QueryBase<List<SubscriptionPaymentDto>>
    {
        public GetSubscriptionPaymentsQuery(Guid payerId)
        {
            PayerId = payerId;
        }

        public Guid PayerId { get; }
    }
}