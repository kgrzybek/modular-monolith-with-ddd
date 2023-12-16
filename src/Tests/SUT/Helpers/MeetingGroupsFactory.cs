using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.SUT.SeedWork;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class MeetingGroupsFactory
    {
        public static async Task<Guid> GivenMeetingGroup(
            IMeetingsModule meetingsModule,
            IAdministrationModule administrationModule,
            string connectionString,
            string name,
            string description,
            string locationCity,
            string locationCountryCode)
        {
            var meetingGroupId = await meetingsModule.ExecuteCommandAsync(new ProposeMeetingGroupCommand(
                name,
                description,
                locationCity,
                locationCountryCode));

            await AsyncOperationsHelper.WaitForProcessing(connectionString);

            await administrationModule.ExecuteCommandAsync(new AcceptMeetingGroupProposalCommand(meetingGroupId));

            await AsyncOperationsHelper.WaitForProcessing(connectionString);

            return meetingGroupId;
        }
    }
}