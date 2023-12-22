using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;
using FluentAssertions;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.MeetingGroupProposals
{
    [TestFixture]
    public class MeetingGroupProposalTests : TestBase
    {
        [Test]
        public void ProposeNewMeetingGroup_IsSuccessful()
        {
            var proposalMemberId = new MemberId(Guid.NewGuid());

            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            var meetingGroupProposed = AssertPublishedDomainEvent<MeetingGroupProposedDomainEvent>(meetingProposal);

            meetingGroupProposed.MeetingGroupProposalId.Should().Be(meetingProposal.Id);
        }

        [Test]
        public void AcceptProposal_WhenIsNotAccepted_IsSuccessful()
        {
            var proposalMemberId = new MemberId(Guid.NewGuid());

            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            var meetingGroupProposalAccepted =
                AssertPublishedDomainEvent<MeetingGroupProposalAcceptedDomainEvent>(meetingProposal);

            meetingGroupProposalAccepted.MeetingGroupProposalId.Should().Be(meetingProposal.Id);
        }

        [Test]
        public void AcceptProposal_WhenIsAlreadyAccepted_BreaksProposalCannotBeAcceptedMoreThanOnceRule()
        {
            var proposalMemberId = new MemberId(Guid.NewGuid());

            var meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            AssertBrokenRule<MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule>(() => { meetingProposal.Accept(); });
        }

        [Test]
        public void CreateMeetingGroup_IsSuccessful_And_CreatorIsAHost()
        {
            var proposalMemberId = new MemberId(Guid.NewGuid());
            var name = "name";
            var description = "description";
            var meetingGroupLocation = MeetingGroupLocation.CreateNew("Warsaw", "PL");
            var meetingProposal = MeetingGroupProposal.ProposeNew(
                name,
                description,
                meetingGroupLocation,
                proposalMemberId);

            var meetingGroup = meetingProposal.CreateMeetingGroup();

            var meetingGroupCreated = AssertPublishedDomainEvent<MeetingGroupCreatedDomainEvent>(meetingGroup);
            var newMeetingGroupMemberJoined =
                AssertPublishedDomainEvent<NewMeetingGroupMemberJoinedDomainEvent>(meetingGroup);

            meetingGroupCreated.MeetingGroupId.Should().Be(meetingProposal.Id);
            newMeetingGroupMemberJoined.MemberId.Should().Be(proposalMemberId);
            newMeetingGroupMemberJoined.Role.Should().Be(MeetingGroupMemberRole.Organizer);
        }
    }
}