using System;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NSubstitute;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingCommentServiceTests : MeetingTestsBase
    {
        [Test]
        public async Task AddLikeToComment_WhenDataIsValid_IsSuccessful()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId, likerId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            var meetingMemberCommentLikesRepository = Substitute.For<IMeetingMemberCommentLikesRepository>();
            meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(likerId, meetingComment.Id).Returns(x => 0);
            var meetingCommentService = new MeetingCommentService(meetingMemberCommentLikesRepository);

            // Act
            var like = await meetingCommentService.AddLikeAsync(meetingTestData.MeetingGroup, meetingComment, likerId);

            // Assert
            var meetingCommentLikedEvent = AssertPublishedDomainEvent<MeetingCommentLikedDomainEvent>(like);
            Assert.That(meetingCommentLikedEvent.MeetingCommentId, Is.EqualTo(meetingComment.Id));
            Assert.That(meetingCommentLikedEvent.LikerId, Is.EqualTo(likerId));
        }

        [Test]
        public void AddLikeToComment_WhenLikerIsNotGroupMember_BreaksCommentCanBeLikedOnlyByMeetingGroupMemberRule()
        {
            // Arrange
            var commentAuthorId = new MemberId(Guid.NewGuid());
            var likerId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions { Attendees = new[] { commentAuthorId } });

            var meetingComment = meetingTestData.Meeting.AddComment(commentAuthorId, "Great meeting!", meetingTestData.MeetingGroup, meetingTestData.MeetingCommentingConfiguration);

            var meetingMemberCommentLikesRepository = Substitute.For<IMeetingMemberCommentLikesRepository>();
            meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(likerId, meetingComment.Id).Returns(x => 0);
            var meetingCommentService = new MeetingCommentService(meetingMemberCommentLikesRepository);

            // Assert
            AssertBrokenRule<CommentCanBeLikedOnlyByMeetingGroupMemberRule>(async () =>
            {
                // Act
                await meetingCommentService.AddLikeAsync(meetingTestData.MeetingGroup, meetingComment, likerId);
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

            var meetingMemberCommentLikesRepository = Substitute.For<IMeetingMemberCommentLikesRepository>();
            meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(likerId, meetingComment.Id).Returns(x => 1);
            var meetingCommentService = new MeetingCommentService(meetingMemberCommentLikesRepository);

            // Assert
            AssertBrokenRule<CommentCannotBeLikedByTheSameMemberMoreThanOnceRule>(async () =>
            {
                // Act
                await meetingCommentService.AddLikeAsync(meetingTestData.MeetingGroup, meetingComment, likerId);
            });
        }
    }
}