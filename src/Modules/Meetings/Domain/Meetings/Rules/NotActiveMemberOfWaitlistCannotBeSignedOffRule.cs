using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class NotActiveMemberOfWaitlistCannotBeSignedOffRule : IBusinessRule
    {
        private readonly List<MeetingWaitlistMember> _waitlistMembers;

        private readonly MemberId _memberId;

        public NotActiveMemberOfWaitlistCannotBeSignedOffRule(List<MeetingWaitlistMember> waitlistMembers, MemberId memberId)
        {
            _waitlistMembers = waitlistMembers;
            _memberId = memberId;
        }

        public bool IsBroken() => _waitlistMembers.SingleOrDefault(x => x.IsActiveOnWaitList(_memberId)) == null;

        public string Message => "Not active member of waitlist cannot be signed off";
    }
}