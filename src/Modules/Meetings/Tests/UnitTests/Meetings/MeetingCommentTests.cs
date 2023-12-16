using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using FluentAssertions;
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
            var comment = "Great meeting!";

            // Act
            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, comment, meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            var meetingCommentCreatedEvent = AssertPublishedDomainEvent<MeetingCommentAddedDomainEvent>(meetingComment);
            meetingCommentCreatedEvent.MeetingCommentId.Should().Be(meetingComment.Id);
            meetingCommentCreatedEvent.MeetingId.Should().Be(meetingTestData.Meeting.Id);
            meetingCommentCreatedEvent.Comment.Should().Be(comment);
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
            meetingCommentEditedEvent.MeetingCommentId.Should().Be(meetingComment.Id);
            meetingCommentEditedEvent.EditedComment.Should().Be(editedComment);
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
            meetingCommentRemovedEvent.MeetingCommentId.Should().Be(meetingComment.Id);
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
        public void AddReplyToComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var replyAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, replyAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            var reply = "Exactly!";

            // Act
            var replyToComment = meetingComment.Reply(replyAuthorId, reply, meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            var replyToMeetingCommentAddedEvent = AssertPublishedDomainEvent<ReplyToMeetingCommentAddedDomainEvent>(replyToComment);
            replyToMeetingCommentAddedEvent.MeetingCommentId.Should().Be(replyToComment.Id);
            replyToMeetingCommentAddedEvent.InReplyToCommentId.Should().Be(meetingComment.Id);
            replyToMeetingCommentAddedEvent.Reply.Should().Be(reply);
        }

        [Test]
        public void AddReplyToComment_WhenAuthorIsNotMeetingGroupMember_BreaksCommentCanBeAddedOnlyByMeetingGroupMemberRule()
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
        public void AddReplyToComment_WhenTextIsEmpty_BreaksCommentTextMustBeProvidedRule(string missingReply)
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
        public void AddReplyToComment_WhenMeetingCommentingDisabled_BreaksCommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule()
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

        [Test]
        public void AddLikeToComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, likerId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Act
            var like = meetingComment.Like(
                likerId,
                likerMeetingGroupMember: new MeetingGroupMemberData(meetingTestData.MeetingGroup.Id, likerId),
                meetingMemberCommentLikesCount: 0);

            // Assert
            var meetingCommentLikedEvent = AssertPublishedDomainEvent<MeetingCommentLikedDomainEvent>(like);
            meetingCommentLikedEvent.MeetingCommentId.Should().Be(meetingComment.Id);
            meetingCommentLikedEvent.LikerId.Should().Be(likerId);
        }

        [Test]
        public void AddLikeToComment_WhenLikerIsNotGroupMember_BreaksCommentCanBeLikedOnlyByMeetingGroupMemberRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            AssertBrokenRule<CommentCanBeLikedOnlyByMeetingGroupMemberRule>(() =>
            {
                // Act
                meetingComment.Like(
                    likerId: new MemberId(Guid.NewGuid()),
                    likerMeetingGroupMember: null,
                    meetingMemberCommentLikesCount: 0);
            });
        }

        [Test]
        public void AddLikeToComment_WhenTheCommentIsAlreadyLikedByTheMember_BreaksCommentCannotBeLikedByTheSameMemberMoreThanOnceRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());

            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, likerId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            // Assert
            AssertBrokenRule<CommentCannotBeLikedByTheSameMemberMoreThanOnceRule>(() =>
            {
                // Act
                meetingComment.Like(
                    likerId,
                    likerMeetingGroupMember: new MeetingGroupMemberData(meetingTestData.MeetingGroup.Id, likerId),
                    meetingMemberCommentLikesCount: 1);
            });
        }

        [Test]
        public void RemoveLike_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, likerId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            var commentLike = meetingComment.Like(
                likerId,
                likerMeetingGroupMember: new MeetingGroupMemberData(meetingTestData.MeetingGroup.Id, likerId),
                meetingMemberCommentLikesCount: 0);
            commentLike.ClearDomainEvents();

            // Act
            commentLike.Remove();

            // Assert
            var meetingCommentUnlikedEvent = AssertPublishedDomainEvent<MeetingCommentUnlikedDomainEvent>(commentLike);
            meetingCommentUnlikedEvent.MeetingCommentId.Should().Be(meetingComment.Id);
            meetingCommentUnlikedEvent.LikerId.Should().Be(likerId);
        }
    }
}