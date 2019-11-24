using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments
{
    public class MoneyValue : ValueObject
    {
        public decimal? Value { get; }

        public string Currency { get; }

        private MoneyValue(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }

        public static MoneyValue Of(decimal? value, string currency)
        {
            return new MoneyValue(value, currency);
        }

        public static bool operator > (decimal left, MoneyValue right) => left > right.Value;

        public static bool operator < (decimal left, MoneyValue right) => left < right.Value;

        public static bool operator >= (decimal left, MoneyValue right) => left >= right.Value;

        public static bool operator <= (decimal left, MoneyValue right) => left <= right.Value;

        public static bool operator > (MoneyValue left, decimal right) => left.Value > right;

        public static bool operator < (MoneyValue left, decimal right) => left.Value < right;

        public static bool operator >= (MoneyValue left, decimal right) => left.Value >= right;

        public static bool operator <= (MoneyValue left, decimal right) => left.Value <= right;
    }
}