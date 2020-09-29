using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules
{
    public class MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly MemberId _enablingMemberId;

        public MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(MemberId enablingMemberId, MeetingGroup meetingGroup)
        {
            _meetingGroup = meetingGroup;
            _enablingMemberId = enablingMemberId;
        }

        public bool IsBroken() => !_meetingGroup.IsOrganizer(_enablingMemberId);

        public string Message => "Commenting for meeting can be enabled only by group organizer";
    }
}