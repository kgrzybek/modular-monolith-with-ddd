namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionDetails
{
    public class SubscriptionDetailsDto
    {
        public Guid SubscriptionId { get; set; }

        public string Period { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Status { get; set; }
    }
}