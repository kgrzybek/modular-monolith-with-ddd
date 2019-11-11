using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MeetingAttendeesLimitCannotBeNegativeRule : IBusinessRule
    {
        private readonly int? _attendeesLimit;

        public MeetingAttendeesLimitCannotBeNegativeRule(int? attendeesLimit)
        {
            _attendeesLimit = attendeesLimit;
        }

        public bool IsBroken() => _attendeesLimit.HasValue && _attendeesLimit.Value < 0;

        public string Message => "Attendees limit cannot be negative";
    }
}