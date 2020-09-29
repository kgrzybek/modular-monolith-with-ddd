using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly MemberId _removingMemberId;
        private readonly string _removingReason;

        public RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule(MeetingGroup meetingGroup, MemberId removingMemberId, string removingReason)
        {
            _meetingGroup = meetingGroup;
            _removingMemberId = removingMemberId;
            _removingReason = removingReason;
        }

        public bool IsBroken() =>
            !string.IsNullOrEmpty(_removingReason) && !_meetingGroup.IsOrganizer(_removingMemberId);

        public string Message => "Only group organizer can provide comment's removing reason.";
    }
}