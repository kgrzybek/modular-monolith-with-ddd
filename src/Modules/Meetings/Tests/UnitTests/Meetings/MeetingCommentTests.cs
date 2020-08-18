using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingCommentTests : MeetingTestsBase
    {
        [Test]
        public void AddComment_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            // Act
            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!");

            // Assert
            var meetingCommentCreated = AssertPublishedDomainEvent<MeetingCommentCreatedDomainEvent>(meetingComment);
            Assert.That(meetingCommentCreated.MeetingCommentId, Is.EqualTo(meetingComment.Id));
        }

        [Test]
        public void EditComment_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!");
            meetingComment.ClearDomainEvents();
            var editedComment = "Wonderful!";

            // Act
            meetingComment.Edit(commentAuthorId, editedComment);

            // Assert
            var meetingCommentCreated = AssertPublishedDomainEvent<MeetingCommentEditedDomainEvent>(meetingComment);
            Assert.That(meetingCommentCreated.MeetingCommentId, Is.EqualTo(meetingComment.Id));
            Assert.That(meetingCommentCreated.EditedComment, Is.EqualTo(editedComment));
        }

        [Test]
        public void AddComment_BreaksCommentCanBeAddedOnlyByAttendeeRule()
        {
            // Arrange
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions()).Meeting;

            // Assert
            AssertBrokenRule<CommentCanBeAddedOnlyByAttendeeRule>(() =>
            {
                // Act
                meeting.AddComment(new MemberId(Guid.NewGuid()), "Bad meeting!");
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void AddComment_BreaksCommentTextMustBeProvidedRule(string missingComment)
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddComment(commentAuthorId, missingComment);
            });
        }

        [Test]
        public void EditComment_BreaksMeetingCommentCanBeEditedOnlyByAuthor()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!");
            meetingComment.ClearDomainEvents();
            var editedComment = "Wonderful!";

            // Assert
            AssertBrokenRule<MeetingCommentCanBeEditedOnlyByAuthor>(() =>
            {
                // Act
                meetingComment.Edit(new MemberId(Guid.NewGuid()), editedComment);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void EditComment_BreaksCommentTextMustBeProvidedRule(string missingComment)
        {
            // Arrange
            var authorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { authorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(authorId, "Great meeting!");
            meetingComment.ClearDomainEvents();

            // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingComment.Edit(new MemberId(Guid.NewGuid()), missingComment);
            });
        }
    }
}