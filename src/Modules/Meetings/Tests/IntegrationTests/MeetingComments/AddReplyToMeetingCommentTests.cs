using CompanyName.MyMeetings.BuildingBlocks.Application;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingComments
{
    [TestFixture]
    public class AddReplyToMeetingCommentTests : TestBase
    {
        [Test]
        public async Task AddReply_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var meetingId = await MeetingHelper.CreateMeetingAsync(MeetingsModule, ExecutionContext);
            var meetingCommentId = await MeetingsModule.ExecuteCommandAsync(new AddMeetingCommentCommand(meetingId, "The meeting was great."));

            var date = new DateTime(2020, 1, 1, 01, 00, 00);
            SystemClock.Set(date);
            var reply = "Absolutely!";

            // Act
            var replyId = await MeetingsModule.ExecuteCommandAsync(new AddReplyToMeetingCommentCommand(meetingCommentId, reply));

            // Assert
            var meetingComments = await MeetingsModule.ExecuteQueryAsync(new GetMeetingCommentsQuery(meetingId));

            Assert.That(meetingComments.Count, Is.EqualTo(2));
            var commentReply = meetingComments.Single(c => c.Id == replyId);
            Assert.That(commentReply.InReplyToCommentId, Is.EqualTo(meetingCommentId));
            Assert.That(commentReply.Comment, Is.EqualTo(reply));
            Assert.That(commentReply.AuthorId, Is.EqualTo(ExecutionContext.UserId));
            Assert.That(commentReply.CreateDate, Is.EqualTo(date));
            Assert.That(commentReply.EditDate, Is.Null);
        }

        [Test]
        public void AddReply_WhenParentCommentNotExists_ThrowsInvalidCommandException()
        {
            // Assert
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                // Act
                await MeetingsModule.ExecuteCommandAsync(new AddReplyToMeetingCommentCommand(
                    inReplyToCommentId: Guid.NewGuid(),
                    "Reply for a nonexistent comment."));
            });
        }
    }
}