using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules
{
    public class NotActualGroupMemberCannotLeaveGroupRule : IBusinessRule
    {
        private readonly List<MeetingGroupMember> _members;

        private readonly MemberId memberId;

        public NotActualGroupMemberCannotLeaveGroupRule(List<MeetingGroupMember> members, MemberId memberId)
            : base()
        {
            _members = members;
            this.memberId = memberId;
        }

        public bool IsBroken() => this._members.SingleOrDefault(x => x.IsMember(memberId)) == null;

        public string Message => "Member is not member of this group so he cannot leave it";
    }
}