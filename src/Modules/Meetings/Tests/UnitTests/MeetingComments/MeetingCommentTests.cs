using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Comments.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.MeetingComments
{
    [TestFixture]
    public class MeetingCommentTests : TestBase
    {
        [Test]
        public void CreateMeetingComment_IsSuccessful()
        {
            var meetingComment = MeetingComment.Create(
                new MeetingId(Guid.NewGuid()),
                new MemberId(Guid.NewGuid()),
                "Great meeting!");
            
            var meetingCommentCreated = AssertPublishedDomainEvent<MeetingCommentCreatedDomainEvent>(meetingComment);

            Assert.That(meetingCommentCreated.MeetingCommentId, Is.EqualTo(meetingComment.Id));
        }
        
        [Test]
        public void CreateMeetingComment_BreaksCommentTextMustBeProvidedRule()
        {
            AssertBrokenRule<CommentTextMustBeProvidedRule>(() =>
            {
                MeetingComment.Create(
                    new MeetingId(Guid.NewGuid()),
                    new MemberId(Guid.NewGuid()),
                    comment: null);
            });
        }
    }
}