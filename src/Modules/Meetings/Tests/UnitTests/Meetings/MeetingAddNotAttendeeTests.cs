using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingAddNotAttendeeTests : MeetingTestsBase
    {
        [Test]
        public void AddNotAttendee_WhenMeetingHasStarted_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.AddNotAttendee(creatorId);
            });
        }

        [Test]
        public void AddNotAttendee_WhenMemberIsAlreadyNotAttendee_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });

            meetingTestData.Meeting.AddNotAttendee(creatorId);

            AssertBrokenRule<MemberCannotBeNotAttendeeTwiceRule>(() =>
            {
                meetingTestData.Meeting.AddNotAttendee(creatorId);
            });
        }

        [Test]
        public void AddNotAttendee_WhenMemberIsNotNotAttendee_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var memberId = new MemberId(Guid.NewGuid());

            meetingTestData.Meeting.AddNotAttendee(memberId);

            var meetingNotAttendeeAdded = AssertPublishedDomainEvent<MeetingNotAttendeeAddedDomainEvent>(meetingTestData.Meeting);
            meetingNotAttendeeAdded.MemberId.Should().Be(memberId);
        }

        [Test]
        public void AddNotAttendee_WhenMemberIsAttendeeAndChangedDecision_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newMemberId, 0);

            meetingTestData.Meeting.AddNotAttendee(newMemberId);

            var meetingAttendeeChangedDecision = AssertPublishedDomainEvent<MeetingAttendeeChangedDecisionDomainEvent>(meetingTestData.Meeting);
            meetingAttendeeChangedDecision.MemberId.Should().Be(newMemberId);
        }

        [Test]
        public void ChangeNotAttendeeDecision_WhenMeetingHasStared_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.ChangeNotAttendeeDecision(newMemberId);
            });
        }

        [Test]
        public void ChangeNotAttendeeDecision_WhenMemberIsNotActiveNotAttendee_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);

            AssertBrokenRule<NotActiveNotAttendeeCannotChangeDecisionRule>(() =>
            {
                meetingTestData.Meeting.ChangeNotAttendeeDecision(newMemberId);
            });
        }

        [Test]
        public void ChangeNotAttendeeDecision_WhenMemberIsNotAttendee_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newMemberId);
            meetingTestData.Meeting.AddNotAttendee(newMemberId);

            meetingTestData.Meeting.ChangeNotAttendeeDecision(newMemberId);

            var meetingNotAttendeeChangedDecision = AssertPublishedDomainEvent<MeetingNotAttendeeChangedDecisionDomainEvent>(meetingTestData.Meeting);
            meetingNotAttendeeChangedDecision.MemberId.Should().Be(newMemberId);
        }
    }
}