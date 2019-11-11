using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingLimits : ValueObject
    {
        public int? AttendeesLimit { get; }

        public int GuestsLimit { get; }

        private MeetingLimits(int? attendeesLimit, int guestsLimit)
        {
            AttendeesLimit = attendeesLimit;
            GuestsLimit = guestsLimit;
        }

        public static MeetingLimits Create(int? attendeesLimit, int guestsLimit)
        {
            CheckRule(new MeetingAttendeesLimitCannotBeNegativeRule(attendeesLimit));

            CheckRule(new MeetingGuestsLimitCannotBeNegativeRule(guestsLimit));

            CheckRule(new MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(attendeesLimit, guestsLimit));

            return new MeetingLimits(attendeesLimit, guestsLimit);
        }
    }
}