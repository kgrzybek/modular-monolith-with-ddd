using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules
{
    public class MeetingGroupMemberCannotBeAddedTwiceRule : IBusinessRule
    {
        private readonly List<MeetingGroupMember> _members;

        private readonly MemberId _memberId;

        public MeetingGroupMemberCannotBeAddedTwiceRule(List<MeetingGroupMember> members, MemberId memberId)
            : base()
        {
            _members = members;
            _memberId = memberId;
        }

        public bool IsBroken() => this._members.SingleOrDefault(x => x.IsMember(_memberId)) != null;

        public string Message => "Member cannot be added twice to the same group";
    }
}