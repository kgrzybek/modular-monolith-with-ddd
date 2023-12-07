using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule : IBusinessRule
    {
        private readonly int? _attendeesLimit;

        private readonly int _guestsLimit;

        public MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(int? attendeesLimit, int guestsLimit)
        {
            _attendeesLimit = attendeesLimit;
            _guestsLimit = guestsLimit;
        }

        public bool IsBroken() => _attendeesLimit.HasValue && _attendeesLimit.Value <= _guestsLimit;

        public string Message => "Attendees limit must be greater than guests limit";
    }
}