using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingCommentServiceTests : MeetingTestsBase
    {
        [Test]
        public async Task AddLikeToComment_WhenTheCommentIsAlreadyLikedByTheMember_BreaksCommentCannotBeLikedByTheSameMemberMoreThanOnceRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());

            var meetingCommentService = new MeetingCommentService();

            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, likerId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            await meetingCommentService.AddLikeAsync(meetingComment, likerId);

            // Assert
            AssertBrokenRule<CommentCannotBeLikedByTheSameMemberMoreThanOnceRule>(async () =>
            {
                // Act
                await meetingCommentService.AddLikeAsync(meetingComment, likerId);
            });
        }
    }
}