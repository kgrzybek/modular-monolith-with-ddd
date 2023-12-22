using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriptionDateExpirationCalculator
    {
        public static DateTime CalculateForNew(SubscriptionPeriod period)
        {
            return SystemClock.Now.AddMonths(period.GetMonthsNumber());
        }

        public static DateTime CalculateForRenewal(DateTime expirationDate, SubscriptionPeriod period)
        {
            if (expirationDate > SystemClock.Now)
            {
                return expirationDate.AddMonths(period.GetMonthsNumber());
            }

            return SystemClock.Now.AddMonths(period.GetMonthsNumber());
        }
    }
}