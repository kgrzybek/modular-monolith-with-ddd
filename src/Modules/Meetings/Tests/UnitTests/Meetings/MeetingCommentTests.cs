using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingCommentTests : MeetingTestsBase
    {
        [Test]
        public void AddComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            // Act
            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            var meetingCommentCreatedEvent = AssertPublishedDomainEvent<MeetingCommentCreatedDomainEvent>(meetingComment);
            Assert.That(meetingCommentCreatedEvent.MeetingCommentId, Is.EqualTo(meetingComment.Id));
        }

        [Test]
        public void AddComment_WhenAuthorIsNotMeetingGroupMember_BreaksCommentCanBeAddedOnlyByMeetingGroupMemberRule()
        {
            // Arrange
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions());

            // Assert
            AssertBrokenRule<CommentCanBeAddedOnlyByMeetingGroupMemberRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddComment(new MemberId(Guid.NewGuid()), "Bad meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void AddComment_WhenTextIsEmpty_BreaksCommentTextMustBeProvidedRule(string missingComment)
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddComment(commentAuthorId, missingComment, meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        public void AddComment_WhenMeetingCommentingDisabled_BreaksCommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(
                new MeetingTestDataOptions
                {
                    Attendees = new[] { commentAuthorId },
                    IsMeetingCommentingEnabled = false
                });

            // Assert
            AssertBrokenRule<CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddComment(commentAuthorId, "I appreciate your work!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        public void EditComment_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();
            var editedComment = "Wonderful!";

            // Act
            meetingComment.Edit(commentAuthorId, editedComment, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            var meetingCommentEditedEvent = AssertPublishedDomainEvent<MeetingCommentEditedDomainEvent>(meetingComment);
            Assert.That(meetingCommentEditedEvent.MeetingCommentId, Is.EqualTo(meetingComment.Id));
            Assert.That(meetingCommentEditedEvent.EditedComment, Is.EqualTo(editedComment));
        }

        [Test]
        public void EditComment_ByNoAuthor_BreaksMeetingCommentCanBeEditedOnlyByAuthor()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();
            var editedComment = "Wonderful!";

            // Assert
            AssertBrokenRule<MeetingCommentCanBeEditedOnlyByAuthorRule>(() =>
            {
                // Act
                meetingComment.Edit(new MemberId(Guid.NewGuid()), editedComment, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void EditComment_WhenNewTextIsEmpty_BreaksCommentTextMustBeProvidedRule(string missingComment)
        {
            // Arrange
            var authorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { authorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(authorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();

            // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingComment.Edit(new MemberId(Guid.NewGuid()), missingComment, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        public void EditComment_WhenMeetingCommentingDisabled_BreaksCommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule()
        {
            // Arrange
            var groupOrganizerId = new MemberId(Guid.NewGuid());
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(
                new MeetingTestDataOptions
                {
                    CreatorId = groupOrganizerId,
                    Attendees = new[] { commentAuthorId }
                });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "It was good.", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingTestData.MeetingCommentingConfiguration.DisableCommenting(groupOrganizerId, meetingTestData.MeetingGroup);

            // Assert
            AssertBrokenRule<CommentCanBeEditedOnlyIfCommentingForMeetingEnabledRule>(() =>
            {
                // Act
                meetingComment.Edit(commentAuthorId, "It was bad.", meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        public void RemoveComment_IsSuccessful()
        {
            // Arrange
            var removingMemberId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            { Attendees = new[] { removingMemberId } });

            var meetingComment = meetingTestData.Meeting.AddComment(removingMemberId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();

            // Act
            meetingComment.Remove(removingMemberId, meetingTestData.MeetingGroup);

            // Assert
            var meetingCommentRemovedEvent = AssertPublishedDomainEvent<MeetingCommentRemovedDomainEvent>(meetingComment);
            Assert.That(meetingCommentRemovedEvent.MeetingCommentId, Is.EqualTo(meetingComment.Id));
        }

        [Test]
        public void RemoveComment_ByNoAuthorNoOrganizer_BreaksMeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var groupCreatorId = new MemberId(Guid.NewGuid());

            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = groupCreatorId,
                Attendees = new[] { commentAuthorId }
            });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();

            // Assert
            AssertBrokenRule<MeetingCommentCanBeRemovedOnlyByAuthorOrGroupOrganizerRule>(() =>
            {
                // Act
                meetingComment.Remove(removingMemberId: new MemberId(Guid.NewGuid()), meetingTestData.MeetingGroup);
            });
        }

        [Test]
        public void RemoveComment_ByAuthor_BreaksRemovingReasonCanBeProvidedOnlyByGroupOrganizer()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData =
                CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            meetingComment.ClearDomainEvents();

            // Assert
            AssertBrokenRule<RemovingReasonCanBeProvidedOnlyByGroupOrganizerRule>(() =>
            {
                // Act
                meetingComment.Remove(
                    commentAuthorId,
                    meetingTestData.MeetingGroup,
                    "I don't like the comment.");
            });
        }

        [Test]
        public void AddCommentReply_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var replyAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, replyAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            var reply = "Exactly!";

            // Act
            var commentReply = meetingComment.Reply(replyAuthorId, reply, meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            var commentReplyCreatedEvent = AssertPublishedDomainEvent<MeetingCommentCreatedDomainEvent>(commentReply);
            Assert.That(commentReplyCreatedEvent.MeetingCommentId, Is.EqualTo(commentReply.Id));
            Assert.That(commentReplyCreatedEvent.InReplyToCommentId, Is.EqualTo(meetingComment.Id));
            Assert.That(commentReplyCreatedEvent.Comment, Is.EqualTo(reply));
        }

        [Test]
        public void AddCommentReply_WhenAuthorIsNotMeetingGroupMember_BreaksCommentCanBeAddedOnlyByMeetingGroupMemberRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            AssertBrokenRule<CommentCanBeAddedOnlyByMeetingGroupMemberRule>(() =>
            {
                // Act
                meetingComment.Reply(replierId: new MemberId(Guid.NewGuid()), "Exactly!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void AddCommentReply_WhenTextIsEmpty_BreaksCommentTextMustBeProvidedRule(string missingReply)
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var replyAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, replyAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingComment.Reply(replyAuthorId, missingReply, meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }

        [Test]
        public void AddCommentReply_WhenMeetingCommentingDisabled_BreaksCommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule()
        {
            // Arrange
            var creatorId = new MemberId(Guid.NewGuid());
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var replyAuthorId = new MemberId(Guid.NewGuid());

            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { CreatorId = creatorId, Attendees = new[] { commentAuthorId, replyAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            meetingTestData.MeetingCommentingConfiguration.DisableCommenting(creatorId, meetingTestData.MeetingGroup);

            // Assert
            AssertBrokenRule<CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule>(() =>
            {
                // Act
                meetingComment.Reply(replyAuthorId, "Exactly!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);
            });
        }
    }
}