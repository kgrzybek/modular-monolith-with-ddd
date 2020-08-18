using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MeetingMustHaveAtLeastOneHostRule : IBusinessRule
    {
        private readonly int _meetingHostNumber;

        public MeetingMustHaveAtLeastOneHostRule(int meetingHostNumber)
        {
            _meetingHostNumber = meetingHostNumber;
        }

        public bool IsBroken() => _meetingHostNumber == 0;

        public string Message => "Meeting must have at least one host";
    }
}