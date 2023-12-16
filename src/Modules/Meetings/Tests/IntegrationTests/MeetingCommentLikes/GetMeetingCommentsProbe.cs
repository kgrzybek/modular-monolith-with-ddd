using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentLikes
{
    public class GetMeetingCommentsProbe : IProbe
    {
        private readonly IMeetingsModule _meetingsModule;

        private readonly Guid _meetingId;

        private readonly Guid _likedMeetingCommentId;

        private readonly int _expectedCommentLikesCount;

        private List<MeetingCommentDto> _meetingComments;

        public GetMeetingCommentsProbe(
            IMeetingsModule meetingsModule,
            Guid meetingId,
            Guid likedMeetingCommentId,
            int expectedCommentLikesCount)
        {
            _meetingsModule = meetingsModule;
            _meetingId = meetingId;
            _likedMeetingCommentId = likedMeetingCommentId;
            _expectedCommentLikesCount = expectedCommentLikesCount;
        }

        public bool IsSatisfied() => _meetingComments != null &&
                                     _meetingComments.Any(c => c.Id == _likedMeetingCommentId
                                                               && c.LikesCount == _expectedCommentLikesCount);

        public async Task SampleAsync()
        {
            _meetingComments = await _meetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(_meetingId));
        }

        public string DescribeFailureTo() => "MeetingComment read model is not in expected state";
    }
}