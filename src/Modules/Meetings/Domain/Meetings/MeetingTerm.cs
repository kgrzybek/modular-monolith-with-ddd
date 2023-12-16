using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingTerm : ValueObject
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public static MeetingTerm CreateNewBetweenDates(DateTime startDate, DateTime endDate)
        {
            return new MeetingTerm(startDate, endDate);
        }

        private MeetingTerm(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal bool IsAfterStart()
        {
            return SystemClock.Now > this.StartDate;
        }
    }
}