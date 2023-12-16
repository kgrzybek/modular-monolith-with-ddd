using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;
using static CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings.MeetingHelper;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingComments
{
    [TestFixture]
    public class EditMeetingCommentTests : TestBase
    {
        [Test]
        public async Task EditMeetingComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var meetingId = await CreateMeetingAsync(MeetingsModule, ExecutionContext);
            var meetingCommentId =
                await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(
                    meetingId,
                    "The meeting was great."));
            var editedComment = "It was very interesting!";

            var meetingCommentsBefore = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));
            var originalMeetingComment = meetingCommentsBefore.Single();

            var date = new DateTime(2020, 1, 1, 01, 00, 00);
            SystemClock.Set(date);

            // Act
            await MeetingsModule.ExecuteCommandAsync(new EditMeetingCommentCommand(meetingCommentId, editedComment));

            // Assert
            var meetingCommentsAfter = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));

            Assert.That(meetingCommentsAfter.Count, Is.EqualTo(1));
            var editedMeetingComment = meetingCommentsAfter.Single();
            Assert.That(editedMeetingComment.Comment, Is.EqualTo(editedComment));
            Assert.That(editedMeetingComment.EditDate, Is.EqualTo(date));
            Assert.That(editedMeetingComment.AuthorId, Is.EqualTo(originalMeetingComment.AuthorId));
            Assert.That(editedMeetingComment.CreateDate, Is.EqualTo(originalMeetingComment.CreateDate));
        }

        [Test]
        public void EditMeetingComment_WhenItIsNonexistent_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new EditMeetingCommentCommand(
                    meetingCommentId: Guid.NewGuid(),
                    "Edit a nonexistent comment."));
            });
        }
    }
}