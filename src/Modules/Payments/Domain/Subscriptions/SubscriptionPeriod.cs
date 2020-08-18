using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.Subscriptions
{
    public class SubscriptionPeriod : ValueObject
    {
        public string Code { get; }

        public static SubscriptionPeriod Month => new SubscriptionPeriod(nameof(Month));

        public static SubscriptionPeriod HalfYear => new SubscriptionPeriod(nameof(HalfYear));

        public static SubscriptionPeriod Of(string code)
        {
            return new SubscriptionPeriod(code);
        }

        public static string GetName(string code)
        {
            return code == Month.Code
                ? "Month"
                : "6 months";
        }

        private SubscriptionPeriod(string code)
        {
            Code = code;
        }

        public int GetMonthsNumber() => this == Month ? 1 : 6;
    }
}