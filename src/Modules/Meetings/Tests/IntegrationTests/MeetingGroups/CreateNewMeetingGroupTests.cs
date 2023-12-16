using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;
using NUnit.Framework;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.MeetingGroups
{
    [TestFixture]
    public class CreateNewMeetingGroupTests : TestBase
    {
        [Test]
        public async Task CreateNewMeetingGroup_Test()
        {
            // Arrange
            var proposalId = await MeetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                MeetingGroupProposalSampleData.Name,
                MeetingGroupProposalSampleData.Description,
                MeetingGroupProposalSampleData.LocationCity,
                MeetingGroupProposalSampleData.LocationCountryCode));

            await MeetingsModule.ExecuteCommandAsync(new AcceptMeetingGroupProposalCommand(Guid.NewGuid(), proposalId));

            // Act
            await MeetingsModule.ExecuteCommandAsync(
                new CreateNewMeetingGroupCommand(
                    Guid.NewGuid(),
                    new MeetingGroupProposalId(proposalId)));

            // Assert
            var meetingGroups = await MeetingsModule.ExecuteQueryAsync(new GetAuthenticationMemberMeetingGroupsQuery());
            Assert.That(meetingGroups, Is.Not.Empty);

            var meetingGroupDetails =
                await MeetingsModule.ExecuteQueryAsync(new GetMeetingGroupDetailsQuery(proposalId));
            Assert.That(meetingGroupDetails.MembersCount, Is.EqualTo(1));
        }
    }
}