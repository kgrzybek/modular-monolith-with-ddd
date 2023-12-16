using CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing;
using CompanyName.MyMeetings.IntegrationTests.SeedWork;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using NUnit.Framework;

namespace CompanyName.MyMeetings.IntegrationTests.CreateMeetingGroup
{
    public class CreateMeetingGroupTests : TestBase
    {
        [Test]
        public async Task CreateMeetingGroupScenario_WhenProposalIsAccepted()
        {
            var meetingGroupId = await MeetingsModule.ExecuteCommandAsync(
                new ProposeMeetingGroupCommand(
                    "Name",
                    "Description",
                    "Location",
                    "PL"));

            await AssertEventually(
                new GetMeetingGroupProposalFromAdministrationProbe(meetingGroupId, AdministrationModule),
                10000);

            await AdministrationModule.ExecuteCommandAsync(new AcceptMeetingGroupProposalCommand(meetingGroupId));

            await AssertEventually(
                new GetCreatedMeetingGroupFromMeetingsProbe(meetingGroupId, MeetingsModule),
                15000);
        }

        private class GetCreatedMeetingGroupFromMeetingsProbe : IProbe
        {
            private readonly Guid _expectedMeetingGroupId;

            private readonly IMeetingsModule _meetingsModule;

            private List<MeetingGroupDto> _allMeetingGroups;

            public GetCreatedMeetingGroupFromMeetingsProbe(
                Guid expectedMeetingGroupId,
                IMeetingsModule meetingsModule)
            {
                _expectedMeetingGroupId = expectedMeetingGroupId;
                _meetingsModule = meetingsModule;
            }

            public bool IsSatisfied()
            {
                return _allMeetingGroups != null &&
                       _allMeetingGroups.Any(x => x.Id == _expectedMeetingGroupId);
            }

            public async Task SampleAsync()
            {
                _allMeetingGroups = await _meetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupsQuery());
            }

            public string DescribeFailureTo()
                => $"Meeting group with ID: {_expectedMeetingGroupId} is not created";
        }

        private class GetMeetingGroupProposalFromAdministrationProbe : IProbe
        {
            private readonly Guid _expectedMeetingGroupProposalId;

            private readonly IAdministrationModule _administrationModule;

            private MeetingGroupProposalDto _meetingGroupProposal;

            public GetMeetingGroupProposalFromAdministrationProbe(
                Guid expectedMeetingGroupProposalId,
                IAdministrationModule administrationModule)
            {
                _expectedMeetingGroupProposalId = expectedMeetingGroupProposalId;
                _administrationModule = administrationModule;
            }

            public bool IsSatisfied()
            {
                if (_meetingGroupProposal == null)
                {
                    return false;
                }

                if (_meetingGroupProposal.Id == _expectedMeetingGroupProposalId &&
                    _meetingGroupProposal.StatusCode == MeetingGroupProposalStatus.ToVerify.Value)
                {
                    return true;
                }

                return false;
            }

            public async Task SampleAsync()
            {
                try
                {
                    _meetingGroupProposal =
                        await _administrationModule.ExecuteQueryAsync(
                            new GetMeetingGroupProposalQuery(_expectedMeetingGroupProposalId));
                }
                catch
                {
                    // ignored
                }
            }

            public string DescribeFailureTo()
                => $"Meeting group proposal with ID: {_expectedMeetingGroupProposalId} to verification not created";
        }
    }
}