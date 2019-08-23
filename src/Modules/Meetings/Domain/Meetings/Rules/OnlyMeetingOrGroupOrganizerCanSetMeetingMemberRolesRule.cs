using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules
{
    public class OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule : IBusinessRule
    {
        private readonly MemberId _settingMemberId;
        private readonly MeetingGroup _meetingGroup;
        private readonly List<MeetingAttendee> _attendees;
        public OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(MemberId settingMemberId, MeetingGroup meetingGroup, List<MeetingAttendee> attendees)
        {
            _settingMemberId = settingMemberId;
            _meetingGroup = meetingGroup;
            _attendees = attendees;
        }

        public bool IsBroken()
        {
            var settingMember = _attendees.SingleOrDefault(x => x.IsActiveAttendee(_settingMemberId));

            var isOrganizer = !_meetingGroup.IsOrganizer(_settingMemberId);

            return settingMember == null || !isOrganizer;
        }

        public string Message => "Only meeting or group organizer can set meeting member roles";
    }
}