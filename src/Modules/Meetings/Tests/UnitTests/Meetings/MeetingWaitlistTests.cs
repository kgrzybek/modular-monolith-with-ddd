using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingWaitlistTests : MeetingTestsBase
    {
        [Test]
        public void SignUpMemberToWaitList_WhenMeetingHasStared_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });
            var memberId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);
            });
        }

        [Test]
        public void SignUpMemberToWaitList_WhenRsvpTermEnded_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                RvspTerm = Term.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });
            var memberId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<AttendeeCanBeAddedOnlyInRsvpTermRule>(() =>
            {
                meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);
            });
        }

        [Test]
        public void SignUpMemberToWaitList_WhenMemberIsNotAMemberOfMeetingGroup_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });

            AssertBrokenRule<MemberOnWaitlistMustBeAMemberOfGroupRule>(() =>
            {
                meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, new MemberId(Guid.NewGuid()));
            });
        }

        [Test]
        public void SignUpMemberToWaitList_WhenMemberIsOnTheListAlready_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var memberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(memberId);

            meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);

            AssertBrokenRule<MemberCannotBeMoreThanOnceOnMeetingWaitlistRule>(() =>
            {
                meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);
            });
        }

        [Test]
        public void SignUpMemberToWaitList_WhenAllConditionsAreSatisfied_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var memberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(memberId);

            meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);

            var meetingWaitlistMemberAdded = AssertPublishedDomainEvent<MeetingWaitlistMemberAddedDomainEvent>(meetingTestData.Meeting);
            meetingWaitlistMemberAdded.MemberId.Should().Be(memberId);
        }

        [Test]
        public void SignOffMemberFromWaitList_WhenMeetingHasStared_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });
            var memberId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.SignOffMemberFromWaitlist(memberId);
            });
        }

        [Test]
        public void SignOffMemberFromWaitList_WhenMemberIsNotActiveWaitlistMember_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var memberId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<NotActiveMemberOfWaitlistCannotBeSignedOffRule>(() =>
            {
                meetingTestData.Meeting.SignOffMemberFromWaitlist(memberId);
            });
        }

        [Test]
        public void SignOffMemberFromWaitList_WhenMemberIsOnWaitList_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });

            var memberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(memberId);
            meetingTestData.Meeting.SignUpMemberToWaitlist(meetingTestData.MeetingGroup, memberId);

            meetingTestData.Meeting.SignOffMemberFromWaitlist(memberId);

            var memberSignedOffFromMeetingWaitlist = AssertPublishedDomainEvent<MemberSignedOffFromMeetingWaitlistDomainEvent>(meetingTestData.Meeting);
            memberSignedOffFromMeetingWaitlist.MemberId.Should().Be(memberId);
        }
    }
}