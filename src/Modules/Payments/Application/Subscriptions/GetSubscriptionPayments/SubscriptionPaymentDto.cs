namespace CompanyName.MyMeetings.Modules.Payments.Application.Subscriptions.GetSubscriptionPayments
{
    public class SubscriptionPaymentDto
    {
        public Guid PaymentId { get; set; }

        public Guid PayerId { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Period { get; set; }

        public DateTime Date { get; set; }

        public Guid? SubscriptionId { get; set; }

        public decimal MoneyValue { get; set; }

        public string MoneyCurrency { get; set; }
    }
}