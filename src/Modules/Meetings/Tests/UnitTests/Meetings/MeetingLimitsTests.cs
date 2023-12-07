using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using FluentAssertions;
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

            meetingLimits.AttendeesLimit.Should().Be(15);
            meetingLimits.GuestsLimit.Should().Be(5);
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

            meetingLimits.AttendeesLimit.Should().BeNull();
            meetingLimits.GuestsLimit.Should().Be(5);
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

        [Test]
        public void CreateMeetingLimits_WhenAttendeesLimitIsEqualToGuestsLimit_BreaksMeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule()
        {
            AssertBrokenRule<MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule>(() =>
            {
                MeetingLimits.Create(5, 5);
            });
        }
    }
}