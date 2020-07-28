using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentTextMustBeProvidedRule : IBusinessRule
    {
        private readonly string _comment;

        public CommentTextMustBeProvidedRule(string comment)
        {
            _comment = comment;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_comment);

        public string Message => "Comment text must be provided.";
    }
}