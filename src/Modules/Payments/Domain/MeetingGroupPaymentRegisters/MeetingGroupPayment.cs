using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class MeetingGroupPayment : Entity
    {
        public MeetingGroupPaymentId Id { get; private set; }

        private DateTime _date;

        private PaymentTerm _term;

        private PayerId _payerId;

        private MeetingGroupPayment()
        {
            // Only for EF.
        }

        internal MeetingGroupPayment(PaymentTerm term, PayerId payerId)
        {
            this.Id = new MeetingGroupPaymentId(Guid.NewGuid());
            _term = term;
            _payerId = payerId;
            _date = DateTime.UtcNow;
        }
    }
}