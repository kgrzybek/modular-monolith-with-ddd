using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingAddCommentTests : MeetingTestsBase
    {
        [Test]
        public void AddComment_IsSuccessful()
        {
            // Arrange
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions());
            var attendeeId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(attendeeId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, attendeeId, 0);
            
            // Act
            var meetingComment =  meetingTestData.Meeting.AddComment(attendeeId, "Great meeting!"); 
            
            // Assert
            var meetingCommentCreated = AssertPublishedDomainEvent<MeetingCommentCreatedDomainEvent>(meetingComment);
            Assert.That(meetingCommentCreated.MeetingCommentId, Is.EqualTo(meetingComment.Id));
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
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions());
            var attendeeId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(attendeeId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, attendeeId, 0);
            
           // Assert
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                // Act
                meetingTestData.Meeting.AddComment(attendeeId, missingComment);
            });
        }
    }
}