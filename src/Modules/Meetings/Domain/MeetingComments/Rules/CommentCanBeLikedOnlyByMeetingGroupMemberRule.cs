using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCanBeLikedOnlyByMeetingGroupMemberRule : IBusinessRule
    {
        private readonly MeetingGroupMemberData _likerMeetingGroupMember;

        #nullable enable
        public CommentCanBeLikedOnlyByMeetingGroupMemberRule(MeetingGroupMemberData? likerMeetingGroupMember)
        {
            _likerMeetingGroupMember = likerMeetingGroupMember;
        }

        public bool IsBroken() => _likerMeetingGroupMember == null;

        public string Message => "Comment can be liked only by meeting group member.";
    }
}