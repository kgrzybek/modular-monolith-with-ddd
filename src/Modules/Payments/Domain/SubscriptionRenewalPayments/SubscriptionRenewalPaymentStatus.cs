namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionRenewalPayments
{
    public class SubscriptionRenewalPaymentStatus
    {
        public static SubscriptionRenewalPaymentStatus WaitingForPayment
            => new SubscriptionRenewalPaymentStatus(nameof(WaitingForPayment));

        public static SubscriptionRenewalPaymentStatus Paid
            => new SubscriptionRenewalPaymentStatus(nameof(Paid));

        public static SubscriptionRenewalPaymentStatus Expired
            => new SubscriptionRenewalPaymentStatus(nameof(Expired));

        public string Code { get; }

        private SubscriptionRenewalPaymentStatus(string code)
        {
            Code = code;
        }

        public static SubscriptionRenewalPaymentStatus Of(string code)
        {
            return new SubscriptionRenewalPaymentStatus(code);
        }
    }
}