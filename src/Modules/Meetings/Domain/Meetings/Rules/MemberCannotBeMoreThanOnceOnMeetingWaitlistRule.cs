using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MemberCannotBeMoreThanOnceOnMeetingWaitlistRule : IBusinessRule
    {
        private readonly List<MeetingWaitlistMember> _waitListMembers;

        private readonly MemberId _memberId;

        internal MemberCannotBeMoreThanOnceOnMeetingWaitlistRule(List<MeetingWaitlistMember> waitListMembers, MemberId memberId)
        {
            _waitListMembers = waitListMembers;
            _memberId = memberId;
        }

        public bool IsBroken() => _waitListMembers.SingleOrDefault(x => x.IsActiveOnWaitList(_memberId)) != null;

        public string Message => "Member cannot be more than once on the meeting waitlist";
    }
}