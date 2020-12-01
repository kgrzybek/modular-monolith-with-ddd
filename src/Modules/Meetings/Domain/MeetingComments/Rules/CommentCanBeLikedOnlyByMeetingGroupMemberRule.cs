using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCanBeLikedOnlyByMeetingGroupMemberRule : IBusinessRule
    {
        private readonly MeetingGroup _meetingGroup;

        private readonly MemberId _likerId;

        public CommentCanBeLikedOnlyByMeetingGroupMemberRule(MeetingGroup meetingGroup, MemberId likerId)
        {
            _meetingGroup = meetingGroup;
            _likerId = likerId;
        }

        public bool IsBroken() => !_meetingGroup.IsMemberOfGroup(_likerId);

        public string Message => "Comment can be liked only by meeting group member.";
    }
}