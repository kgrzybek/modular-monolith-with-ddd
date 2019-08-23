using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingTerm : ValueObject
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public MeetingTerm(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal bool IsInTerm(DateTime date)
        {
            var left = this.StartDate <= date;

            var right =  this.EndDate >= date;

            return left && right;
        }

        public bool IsAfterStart()
        {
            return DateTime.UtcNow > this.StartDate;
        }
    }
}