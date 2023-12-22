namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.AggregateStore
{
    public interface ICheckpointStore
    {
        long? GetCheckpoint(SubscriptionCode subscriptionCode);

        Task StoreCheckpoint(SubscriptionCode subscriptionCode, long checkpoint);
    }
}