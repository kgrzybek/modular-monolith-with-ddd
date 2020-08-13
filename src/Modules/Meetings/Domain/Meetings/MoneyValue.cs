using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MoneyValue : ValueObject
    {
        public static MoneyValue Undefined => new MoneyValue(null, null);

        public decimal? Value { get; }

        public string Currency { get; }

        public static MoneyValue Of(decimal value, string currency)
        {
            return new MoneyValue(value, currency);
        }

        private MoneyValue(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }

        public static MoneyValue operator *(int left, MoneyValue right)
        {
            return new MoneyValue(right.Value * left, right.Currency);
        }
    }
}