using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules
{
    public class MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly MemberId _disablingMemberId;

        public MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(MemberId disablingMemberId, MeetingGroup meetingGroup)
        {
            _meetingGroup = meetingGroup;
            _disablingMemberId = disablingMemberId;
        }

        public bool IsBroken() => !_meetingGroup.IsOrganizer(_disablingMemberId);

        public string Message => "Commenting for meeting can be disabled only by group organizer";
    }
}