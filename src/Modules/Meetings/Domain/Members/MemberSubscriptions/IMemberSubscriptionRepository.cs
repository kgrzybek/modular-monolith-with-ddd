namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions
{
    public interface IMemberSubscriptionRepository
    {
        Task<MemberSubscription> GetByIdOptionalAsync(MemberSubscriptionId memberSubscriptionId);

        Task AddAsync(MemberSubscription memberSubscription);
    }
}