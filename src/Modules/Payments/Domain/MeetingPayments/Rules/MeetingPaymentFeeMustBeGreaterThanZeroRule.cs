using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments.Rules
{
    public class MeetingPaymentFeeMustBeGreaterThanZeroRule : IBusinessRule
    {
        private readonly MoneyValue _fee;

        internal MeetingPaymentFeeMustBeGreaterThanZeroRule(MoneyValue fee)
        {
            _fee = fee;
        }

        public bool IsBroken() => _fee <= 0;

        public string Message => "Meeting payment fee must be greater than zero";
    }
}