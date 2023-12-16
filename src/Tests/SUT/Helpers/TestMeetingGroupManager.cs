using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class TestMeetingGroupManager
    {
        internal static async Task JoinToGroup(IMeetingsModule meetingsModule, Guid meetingGroupId)
        {
            await meetingsModule.ExecuteCommandAsync(new JoinToGroupCommand(meetingGroupId));
        }
    }
}