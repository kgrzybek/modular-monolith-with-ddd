using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.MeetingGroups
{
    [TestFixture]
    public class MeetingGroupTests : TestBase
    {
        [Test]
        public void EditGeneralAttributes_IsSuccessful()
        {
            var meetingGroup = CreateMeetingGroup();

            var meetingGroupLocation = MeetingGroupLocation.CreateNew("London", "GB");
            meetingGroup.EditGeneralAttributes("newName", "newDescription", meetingGroupLocation);

            var meetingGroupGeneralAttributesEdited =
                AssertPublishedDomainEvent<MeetingGroupGeneralAttributesEditedDomainEvent>(meetingGroup);

            meetingGroupGeneralAttributesEdited.NewName.Should().Be("newName");
            meetingGroupGeneralAttributesEdited.NewDescription.Should().Be("newDescription");
            meetingGroupGeneralAttributesEdited.NewLocation.Should().Be(meetingGroupLocation);
        }

        [Test]
        public void JoinToGroup_WhenMemberHasNotJoinedYet_IsSuccessful()
        {
            var meetingGroup = CreateMeetingGroup();

            MemberId newMemberId = new MemberId(Guid.NewGuid());
            meetingGroup.JoinToGroupMember(newMemberId);

            var newMeetingGroupMemberJoined = AssertPublishedDomainEvent<NewMeetingGroupMemberJoinedDomainEvent>(meetingGroup);

            newMeetingGroupMemberJoined.MeetingGroupId.Should().Be(meetingGroup.Id);
            newMeetingGroupMemberJoined.MemberId.Should().Be(newMemberId);
            newMeetingGroupMemberJoined.Role.Should().Be(MeetingGroupMemberRole.Member);
        }

        [Test]
        public void JoinToGroup_WhenMemberHasAlreadyJoined_BreaksMeetingGroupMemberCannotBeAddedTwiceRule()
        {
            var meetingGroup = CreateMeetingGroup();

            MemberId newMemberId = new MemberId(Guid.NewGuid());
            meetingGroup.JoinToGroupMember(newMemberId);

            AssertBrokenRule<MeetingGroupMemberCannotBeAddedTwiceRule>(() =>
            {
                meetingGroup.JoinToGroupMember(newMemberId);
            });
        }

        [Test]
        public void LeaveGroup_WhenMemberIsActiveMemberOfGroup_IsSuccessful()
        {
            var meetingGroup = CreateMeetingGroup();
            var newMemberId = new MemberId(Guid.NewGuid());
            meetingGroup.JoinToGroupMember(newMemberId);

            meetingGroup.LeaveGroup(newMemberId);

            var meetingGroupMemberLeft = AssertPublishedDomainEvent<MeetingGroupMemberLeftGroupDomainEvent>(meetingGroup);

            meetingGroupMemberLeft.MemberId.Should().Be(newMemberId);
        }

        [Test]
        public void LeaveGroup_WhenMemberIsNotActiveMemberOfGroup_BreaksNotActualGroupMemberCannotLeaveGroupRule()
        {
            var meetingGroup = CreateMeetingGroup();
            var newMemberId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<NotActualGroupMemberCannotLeaveGroupRule>(() =>
            {
                meetingGroup.LeaveGroup(newMemberId);
            });
        }

        [Test]
        public void UpdatePaymentDateTo_IsSuccessful()
        {
            var meetingGroup = CreateMeetingGroup();
            var dateTo = DateTime.UtcNow;

            meetingGroup.SetExpirationDate(dateTo);

            var meetingGroupPaymentInfoUpdated = AssertPublishedDomainEvent<MeetingGroupPaymentInfoUpdatedDomainEvent>(meetingGroup);

            meetingGroupPaymentInfoUpdated.MeetingGroupId.Should().Be(meetingGroup.Id);
            meetingGroupPaymentInfoUpdated.PaymentDateTo.Should().Be(dateTo);
        }

        [Test]
        public void CreateMeeting_WhenGroupIsNotPayed_IsNotPossible()
        {
            var meetingGroup = CreateMeetingGroup();

            MemberId creatorId = new MemberId(Guid.NewGuid());

            AssertBrokenRule<MeetingCanBeOrganizedOnlyByPayedGroupRule>(() =>
            {
                meetingGroup.CreateMeeting(
                    "title",
                    MeetingTerm.CreateNewBetweenDates(
                        new DateTime(2019, 1, 1, 10, 0, 0),
                        new DateTime(2019, 1, 1, 12, 0, 0)),
                    "description",
                    MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                    null,
                    0,
                    Term.NoTerm,
                    MoneyValue.Undefined,
                    [],
                    creatorId);
            });
        }

        [Test]
        public void CreateMeeting_WhenCreatorIsMemberOfGroupAndHostsAreNotDefined_IsSuccessful()
        {
            var definedProposalMemberId = new MemberId(Guid.NewGuid());
            var meetingGroup = CreateMeetingGroup(definedProposalMemberId);
            meetingGroup.SetExpirationDate(DateTime.UtcNow.AddDays(1));

            var meeting = meetingGroup.CreateMeeting(
                "title",
                MeetingTerm.CreateNewBetweenDates(
                    new DateTime(2019, 1, 1, 10, 0, 0),
                    new DateTime(2019, 1, 1, 12, 0, 0)),
                "description",
                MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                null,
                0,
                Term.NoTerm,
                MoneyValue.Undefined,
                [],
                definedProposalMemberId);

            AssertPublishedDomainEvent<MeetingCreatedDomainEvent>(meeting);
        }

        [Test]
        public void CreateMeeting_WhenHostsAreDefinedAndTheyAreGroupMembers_DefinedHostsAreHostsOfMeeting()
        {
            var definedProposalMemberId = new MemberId(Guid.NewGuid());
            var meetingGroup = CreateMeetingGroup(definedProposalMemberId);
            meetingGroup.SetExpirationDate(DateTime.UtcNow.AddDays(1));
            var hostOne = new MemberId(Guid.NewGuid());
            var hostTwo = new MemberId(Guid.NewGuid());
            List<MemberId> hosts =
            [
                hostOne,
                hostTwo
            ];
            meetingGroup.JoinToGroupMember(hostOne);
            meetingGroup.JoinToGroupMember(hostTwo);

            var meeting = meetingGroup.CreateMeeting(
                "title",
                MeetingTerm.CreateNewBetweenDates(
                    new DateTime(2019, 1, 1, 10, 0, 0),
                    new DateTime(2019, 1, 1, 12, 0, 0)),
                "description",
                MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                null,
                0,
                Term.NoTerm,
                MoneyValue.Undefined,
                hosts,
                definedProposalMemberId);

            var meetingAttendeeAddedEvents = AssertPublishedDomainEvents<MeetingAttendeeAddedDomainEvent>(meeting);

            meetingAttendeeAddedEvents.Should().HaveCount(2);
            meetingAttendeeAddedEvents[0].AttendeeId.Should().Be(hostOne);
            meetingAttendeeAddedEvents[0].Role.Should().Be(MeetingAttendeeRole.Host.Value);
            meetingAttendeeAddedEvents[1].AttendeeId.Should().Be(hostTwo);
            meetingAttendeeAddedEvents[1].Role.Should().Be(MeetingAttendeeRole.Host.Value);
        }

        [Test]
        public void CreateMeeting_WhenHostsAreDefinedAndTheyAreNotGroupMembers_BreaksMeetingHostMustBeAMeetingGroupMemberRule()
        {
            var definedProposalMemberId = new MemberId(Guid.NewGuid());
            var meetingGroup = CreateMeetingGroup(definedProposalMemberId);
            meetingGroup.SetExpirationDate(DateTime.UtcNow.AddDays(1));
            var hostOne = new MemberId(Guid.NewGuid());
            var hostTwo = new MemberId(Guid.NewGuid());
            List<MemberId> hosts =
            [
                hostOne,
                hostTwo
            ];

            AssertBrokenRule<MeetingHostMustBeAMeetingGroupMemberRule>(() =>
            {
                meetingGroup.CreateMeeting(
                    "title",
                    MeetingTerm.CreateNewBetweenDates(
                        new DateTime(2019, 1, 1, 10, 0, 0),
                        new DateTime(2019, 1, 1, 12, 0, 0)),
                    "description",
                    MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                    null,
                    0,
                    Term.NoTerm,
                    MoneyValue.Undefined,
                    hosts,
                    definedProposalMemberId);
            });
        }

        [Test]
        public void CreateMeeting_WhenCreatorIsNotMemberOfGroup_BreaksMeetingHostMustBeAMeetingGroupMemberRule()
        {
            var definedProposalMemberId = new MemberId(Guid.NewGuid());
            var creatorId = new MemberId(Guid.NewGuid());
            var meetingGroup = CreateMeetingGroup(definedProposalMemberId);
            meetingGroup.SetExpirationDate(DateTime.UtcNow.AddDays(1));

            AssertBrokenRule<MeetingHostMustBeAMeetingGroupMemberRule>(() =>
            {
                meetingGroup.CreateMeeting(
                    "title",
                    MeetingTerm.CreateNewBetweenDates(
                        new DateTime(2019, 1, 1, 10, 0, 0),
                        new DateTime(2019, 1, 1, 12, 0, 0)),
                    "description",
                    MeetingLocation.CreateNew("Name", "Address", "PostalCode", "City"),
                    null,
                    0,
                    Term.NoTerm,
                    MoneyValue.Undefined,
                    [],
                    creatorId);
            });
        }

        private static MeetingGroup CreateMeetingGroup(MemberId definedProposalMemberId = null)
        {
            var proposalMemberId = definedProposalMemberId ?? new MemberId(Guid.NewGuid());
            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            var meetingGroup = meetingProposal.CreateMeetingGroup();

            DomainEventsTestHelper.ClearAllDomainEvents(meetingGroup);

            return meetingGroup;
        }
    }
}