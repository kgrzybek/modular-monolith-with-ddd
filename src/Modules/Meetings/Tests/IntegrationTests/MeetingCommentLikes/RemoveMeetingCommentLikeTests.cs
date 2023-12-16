using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentLikes
{
    [TestFixture]
    public class RemoveMeetingCommentLikeTests : TestBase
    {
        [Test]
        public async Task UnlikeMeetingComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            await MeetingsModule.ExecuteCommandAsync(
                new CreateMemberCommand(
                    Guid.NewGuid(),
                    ExecutionContext.UserId,
                    "ivan_petrov",
                    "ivan@mail.com",
                    "Ivan",
                    "Petrov",
                    "Ivan Petrov"));

            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);

            var meetingCommentId = await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(meetingId, "The meeting was awesome."));

            await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentLikeCommand(meetingCommentId));

            await AssertEventually(
                new GetMeetingCommentsProbe(MeetingsModule, meetingId, meetingCommentId, expectedCommentLikesCount: 1),
                10000);

            // Act
            await MeetingsModule.ExecuteCommandAsync(new RemoveMeetingCommentLikeCommand(meetingCommentId));

            // Assert
            var meetingCommentLikers = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentLikersQuery(meetingCommentId));
            Assert.That(meetingCommentLikers.Count, Is.EqualTo(0));

            await AssertEventually(
                new GetMeetingCommentsProbe(MeetingsModule, meetingId, meetingCommentId, expectedCommentLikesCount: 0),
                10000);
        }

        [Test]
        public void UnlikeMeetingComment_WhenCommentNotExists_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new RemoveMeetingCommentLikeCommand(meetingCommentId: Guid.NewGuid()));
            });
        }
    }
}