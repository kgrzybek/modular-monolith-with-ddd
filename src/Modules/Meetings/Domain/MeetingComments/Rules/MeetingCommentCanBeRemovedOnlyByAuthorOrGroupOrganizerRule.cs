using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;
        private readonly MemberId _authorId;
        private readonly MemberId _removingMemberId;

        public MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule(MeetingGroup meetingGroup, MemberId authorId, MemberId removingMemberId)
        {
            _meetingGroup = meetingGroup;
            _authorId = authorId;
            _removingMemberId = removingMemberId;
        }

        public bool IsBroken() => _removingMemberId != _authorId && !_meetingGroup.IsOrganizer(_removingMemberId);

        public string Message => "Only author of comment or group organizer can remove meeting comment.";
    }
}