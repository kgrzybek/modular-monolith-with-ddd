using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingAddCommentTests : MeetingTestsBase
    {
        [Test]
        public void AddComment_BreaksCommentCanBeAddedOnlyByAttendeeRule()
        {
            var meeting = CreateMeetingTestData(new MeetingTestDataOptions()).Meeting;

            AssertBrokenRule<CommentCanBeAddedOnlyByAttendeeRule>(() =>
            {
                meeting.AddComment(new MemberId(Guid.NewGuid()), "Great meeting!");
            });
        }
    }
}