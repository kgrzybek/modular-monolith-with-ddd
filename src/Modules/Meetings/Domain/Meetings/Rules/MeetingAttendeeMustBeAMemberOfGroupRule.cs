using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class MeetingAttendeeMustBeAMemberOfGroupRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;

        private readonly MemberId _attendeeId;

        internal MeetingAttendeeMustBeAMemberOfGroupRule(MemberId attendeeId, MeetingGroup meetingGroup)
        {
            _attendeeId = attendeeId;
            _meetingGroup = meetingGroup;
        }

        public bool IsBroken()
        {
            return !_meetingGroup.IsMemberOfGroup(_attendeeId);
        }

        public string Message => "Meeting attendee must be a member of group";
    }
}