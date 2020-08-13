using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule : IBusinessRule
    {
        private readonly string _reason;

        internal ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(string reason)
        {
            _reason = reason;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_reason);

        public string Message => "Reason of removing attendee from meeting must be provided";
    }
}