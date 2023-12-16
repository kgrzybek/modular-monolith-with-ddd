using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingComments
{
    [TestFixture]
    public class AddMeetingCommentTests : TestBase
    {
        [Test]
        public async Task AddMeetingComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);

            var date = new DateTime(2020, 1, 1, 01, 00, 00);
            SystemClock.Set(date);
            var comment = "The meeting was great.";

            // Act
            var meetingCommentId =
                await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(meetingId, comment));

            // Assert
            var meetingComments = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));

            Assert.That(meetingComments.Count, Is.EqualTo(1));
            var meetingComment = meetingComments.Single();
            Assert.That(meetingComment.Id, Is.EqualTo(meetingCommentId));
            Assert.That(meetingComment.Comment, Is.EqualTo(comment));
            Assert.That(meetingComment.AuthorId, Is.EqualTo(ExecutionContext.UserId));
            Assert.That(meetingComment.CreateDate, Is.EqualTo(date));
            Assert.That(meetingComment.EditDate, Is.Null);
        }

        [Test]
        public void AddMeetingComment_WhenMeetingIsNonexistent_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(
                    meetingId: Guid.NewGuid(),
                    "Comment for a nonexistent meeting."));
            });
        }
    }
}