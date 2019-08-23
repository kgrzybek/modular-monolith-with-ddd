using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MoneyValue : ValueObject
    {
        public static MoneyValue Zero => new MoneyValue(null, null);

        public decimal? Value { get; }

        public string Currency { get; }

        public MoneyValue(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }

        public static MoneyValue operator * (int left, MoneyValue right) {
            
            return new MoneyValue(right.Value * left, right.Currency);
        }
    }
}