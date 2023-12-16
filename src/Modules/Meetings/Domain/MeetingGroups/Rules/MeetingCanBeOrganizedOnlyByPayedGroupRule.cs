using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules
{
    public class MeetingCanBeOrganizedOnlyByPayedGroupRule : IBusinessRule
    {
        private readonly DateTime? _paymentDateTo;

        internal MeetingCanBeOrganizedOnlyByPayedGroupRule(DateTime? paymentDateTo)
        {
            _paymentDateTo = paymentDateTo;
        }

        public bool IsBroken() => !_paymentDateTo.HasValue || _paymentDateTo < SystemClock.Now;

        public string Message => "Meeting can be organized only by payed group";
    }
}