using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters.Rules
{
    public class MeetingGroupPaymentsCannotOverlapRule : IBusinessRule
    {
        private readonly List<MeetingGroupPayment> _payments;
        private readonly MeetingGroupPayment _newPayment;

        public MeetingGroupPaymentsCannotOverlapRule(List<MeetingGroupPayment> payments, MeetingGroupPayment newPayment)
        {
            _payments = payments;
            _newPayment = newPayment;
        }

        public bool IsBroken() => _payments.Any(x => x.OverlapsWith(_newPayment));

        public string Message => "Meeting group payment overlaps with other payment";
    }
}