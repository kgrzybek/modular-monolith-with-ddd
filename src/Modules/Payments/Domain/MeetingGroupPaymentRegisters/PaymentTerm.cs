using System;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class PaymentTerm
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public PaymentTerm(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}