using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments
{
    public class MoneyValue : ValueObject
    {
        public decimal? Value { get; }

        public string Currency { get; }

        public MoneyValue(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }
    }
}