using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments.Rules
{
    public class MeetingPaymentCannotBePayedTwiceRule : IBusinessRule
    {
        private readonly DateTime? _paymentDate;

        internal MeetingPaymentCannotBePayedTwiceRule(DateTime? paymentDate)
        {
            _paymentDate = paymentDate;
        }

        public bool IsBroken() => _paymentDate.HasValue;

        public string Message => "Meeting payment cannot be payed twice";
    }
}