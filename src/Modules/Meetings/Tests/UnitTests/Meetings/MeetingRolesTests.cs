using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.Meetings
{
    [TestFixture]
    public class MeetingRolesTests : MeetingTestsBase
    {
        [Test]
        public void SetHostRole_WhenMeetingHasStarted_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, creatorId, creatorId);
            });
        }

        [Test]
        public void SetHostRole_WhenSettingMemberIsNotAOrganizerOrHostMeeting_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var settingMemberId = new MemberId(Guid.NewGuid());
            var newOrganizerId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newOrganizerId);
            meetingTestData.MeetingGroup.JoinToGroupMember(settingMemberId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newOrganizerId, 0);

            AssertBrokenRule<OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule>(() =>
            {
                meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, settingMemberId, newOrganizerId);
            });
        }

        [Test]
        public void SetHostRole_WhenSettingMemberIsGroupOrganizer_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newOrganizerId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newOrganizerId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newOrganizerId, 0);

            meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, creatorId, newOrganizerId);

            var newMeetingHostSet = AssertPublishedDomainEvent<NewMeetingHostSetDomainEvent>(meetingTestData.Meeting);
            newMeetingHostSet.HostId.Should().Be(newOrganizerId);
        }

        [Test]
        public void SetHostRole_WhenSettingMemberIsMeetingHost_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newOrganizerId = new MemberId(Guid.NewGuid());
            var settingMemberId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newOrganizerId);
            meetingTestData.MeetingGroup.JoinToGroupMember(settingMemberId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newOrganizerId, 0);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, settingMemberId, 0);
            meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, creatorId, settingMemberId);
            DomainEventsTestHelper.ClearAllDomainEvents(meetingTestData.Meeting);

            meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, settingMemberId, newOrganizerId);

            var newMeetingHostSetEvent = AssertPublishedDomainEvent<NewMeetingHostSetDomainEvent>(meetingTestData.Meeting);
            newMeetingHostSetEvent.HostId.Should().Be(newOrganizerId);
        }

        [Test]
        public void SetAttendeeRole_WhenMeetingHasStarted_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId,
                MeetingTerm = MeetingTerm.CreateNewBetweenDates(DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1))
            });

            AssertBrokenRule<MeetingCannotBeChangedAfterStartRule>(() =>
            {
                meetingTestData.Meeting.SetAttendeeRole(meetingTestData.MeetingGroup, creatorId, creatorId);
            });
        }

        [Test]
        public void SetAttendeeRole_WhenSettingMemberIsNotAOrganizerOrHostMeeting_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var settingMemberId = new MemberId(Guid.NewGuid());
            var newOrganizerId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newOrganizerId);
            meetingTestData.MeetingGroup.JoinToGroupMember(settingMemberId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newOrganizerId, 0);

            AssertBrokenRule<OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule>(() =>
            {
                meetingTestData.Meeting.SetAttendeeRole(meetingTestData.MeetingGroup, settingMemberId, newOrganizerId);
            });
        }

        [Test]
        public void SetAttendeeRole_WhenMemberIsOrganizer_IsSuccessful()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var newOrganizerId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(newOrganizerId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, newOrganizerId, 0);
            meetingTestData.Meeting.SetHostRole(meetingTestData.MeetingGroup, creatorId, newOrganizerId);

            meetingTestData.Meeting.SetAttendeeRole(meetingTestData.MeetingGroup, creatorId, newOrganizerId);

            var newMeetingHostSet = AssertPublishedDomainEvent<MemberSetAsAttendeeDomainEvent>(meetingTestData.Meeting);
            newMeetingHostSet.HostId.Should().Be(newOrganizerId);
        }

        [Test]
        public void SetAttendeeRole_WhenMemberIsAlreadyAttendee_IsNotPossible()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });
            var attendeeId = new MemberId(Guid.NewGuid());
            meetingTestData.MeetingGroup.JoinToGroupMember(attendeeId);
            meetingTestData.Meeting.AddAttendee(meetingTestData.MeetingGroup, attendeeId, 0);

            AssertBrokenRule<MemberCannotHaveSetAttendeeRoleMoreThanOnceRule>(() =>
            {
                meetingTestData.Meeting.SetAttendeeRole(meetingTestData.MeetingGroup, creatorId, attendeeId);
            });
        }

        [Test]
        public void SetAttendeeRole_ForLastOrganizer_BreaksMeetingMustHaveAtLeastOneHostRule()
        {
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingTestData = CreateMeetingTestData(new MeetingTestDataOptions
            {
                CreatorId = creatorId
            });

            AssertBrokenRule<MeetingMustHaveAtLeastOneHostRule>(() =>
            {
                meetingTestData.Meeting.SetAttendeeRole(meetingTestData.MeetingGroup, creatorId, creatorId);
            });
        }
    }
}