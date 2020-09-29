using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class MeetingCommentCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly MemberId _authorId;
        private readonly MemberId _editorId;

        public MeetingCommentCanBeEditedOnlyByAuthorRule(MemberId authorId, MemberId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken() => _editorId != _authorId;

        public string Message => "Only the author of a comment can edit it.";
    }
}