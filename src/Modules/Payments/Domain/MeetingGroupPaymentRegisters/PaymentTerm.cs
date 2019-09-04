using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters
{
    public class PaymentTerm : ValueObject
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public PaymentTerm(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal bool OverlapsWith(PaymentTerm term)
        {
            return this.IsBetween(term.StartDate) ||
                    this.IsBetween(term.EndDate) ||
                    term.IsBetween(this.StartDate) ||
                    term.IsBetween(this.EndDate);
        }

        private bool IsBetween(DateTime date)
        {
            return this.StartDate <= date && date <= this.EndDate;
        }
    }
}