using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;
using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;
using CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.SeedWork;

namespace CompanyName.MyMeetings.Modules.Meetings.IntegrationTests.Meetings
{
    internal static class MeetingHelper
    {
        public static async Task<Guid> CreateMeetingAsync(
            IMeetingsModule meetingsModule,
            ExecutionContextMock executionContext)
        {
            var proposalId = await meetingsModule.ExecuteCommandAsync(
                new ProposeMeetingGroupCommand(
                    "Amazing group",
                    "Absolutely amazing meeting group.",
                    "London",
                    "UK"));

            await meetingsModule.ExecuteCommandAsync(
                new CreateNewMeetingGroupCommand(
                    Guid.NewGuid(),
                    new MeetingGroupProposalId(proposalId)));

            var meetingGroups = await meetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupsQuery());
            var meetingGroup = meetingGroups.Single();

            await meetingsModule.ExecuteCommandAsync(
                new SetMeetingGroupExpirationDateCommand(
                    Guid.NewGuid(),
                    meetingGroup.Id,
                    SystemClock.Now.AddMonths(1)));

            var meetingId = await meetingsModule.ExecuteCommandAsync(
                new CreateMeetingCommand(
                    meetingGroup.Id,
                    "Some meeting",
                    DateTime.UtcNow.AddDays(1),
                    DateTime.UtcNow.AddDays(10),
                    "Some very nice meeting.",
                    "UK",
                    "Baker street",
                    "W2 2SZ",
                    "London",
                    25,
                    1,
                    null,
                    null,
                    null,
                    null,
                    [executionContext.UserId]));

            return meetingId;
        }
    }
}