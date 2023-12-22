using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingCommentLikes
{
    [TestFixture]
    public class AddMeetingCommentLikeTests : TestBase
    {
        [Test]
        public async Task AddMeetingCommentLike_WhenDataIsValid_IsSuccess()
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

            // Act
            await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentLikeCommand(meetingCommentId));

            // Assert
            var meetingCommentLikers = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentLikersQuery(meetingCommentId));
            Assert.That(meetingCommentLikers.Count, Is.EqualTo(1));
            Assert.That(meetingCommentLikers.Single().Id, Is.EqualTo(ExecutionContext.UserId));

            await AssertEventually(
                new GetMeetingCommentsProbe(MeetingsModule, meetingId, meetingCommentId, expectedCommentLikesCount: 1),
                10000);
        }

        [Test]
        public void AddMeetingCommentLike_WhenCommentNotExists_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentLikeCommand(meetingCommentId: Guid.NewGuid()));
            });
        }
    }
}