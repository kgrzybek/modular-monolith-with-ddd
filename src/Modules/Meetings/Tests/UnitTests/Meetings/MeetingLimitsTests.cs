using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingLimitsTests : MeetingTestsBase
    {
        [Test]
        public void CreateMeetingLimits_WhenAttendeesLimitIsGreaterThanGuestsLimit_IsSuccessful()
        {
            var meetingLimits = MeetingLimits.Create(15, 5);

            Assert.That(meetingLimits.AttendeesLimit, Is.EqualTo(15));
            Assert.That(meetingLimits.GuestsLimit, Is.EqualTo(5));
        }

        [Test]
        public void CreateMeetingLimits_WhenAttendeesLimitIsLessThanGuestsLimit_BreaksMeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule()
        {
            AssertBrokenRule<MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule>(() =>
            {
                MeetingLimits.Create(5, 8);
            }); 
        }

        [Test]
        public void CreateMeetingLimits_WhenAttendeesLimitIsNotDefined_GuestsLimitCanBeAny()
        {
            var meetingLimits = MeetingLimits.Create(null, 5);

            Assert.That(meetingLimits.AttendeesLimit, Is.EqualTo(null));
            Assert.That(meetingLimits.GuestsLimit, Is.EqualTo(5));
        }

        [Test]
        public void CreateMeetingLimits_WhenAttendeesLimitIsNegative_BreaksMeetingAttendeesLimitCannotBeNegativeRule()
        {
            AssertBrokenRule<MeetingAttendeesLimitCannotBeNegativeRule>(() =>
            {
                MeetingLimits.Create(-2, 8);
            }); 
        }

        [Test]
        public void CreateMeetingLimits_WhenGuestsLimitIsNegative_BreaksMeetingGuestsLimitCannotBeNegativeRule()
        {
            AssertBrokenRule<MeetingGuestsLimitCannotBeNegativeRule>(() =>
            {
                MeetingLimits.Create(20, -9);
            }); 
        }
    }
}