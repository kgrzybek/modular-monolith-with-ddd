using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupPayment : Entity
    {
        internal MeetingGroupPaymentId Id { get; private set; }

        private DateTime _date;

        private PaymentTerm _term;

        private PayerId _payerId;

        private MeetingGroupPayment()
        {
            // Only for EF.
        }

        private MeetingGroupPayment(PaymentTerm term, PayerId payerId)
        {
            this.Id = new MeetingGroupPaymentId(Guid.NewGuid());
            _term = term;
            _payerId = payerId;
            _date = SystemClock.Now;
        }

        internal static MeetingGroupPayment CreateForTerm(PaymentTerm term, PayerId payerId)
        {
            return new MeetingGroupPayment(term, payerId);
        }

        internal bool OverlapsWith(MeetingGroupPayment payment)
        {
            return _term.OverlapsWith(payment.GetTerm());
        }

        private PaymentTerm GetTerm()
        {
            return _term;
        }
    }
}