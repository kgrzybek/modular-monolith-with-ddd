using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingAttendee;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class TestMeetingManager
    {
        internal static async Task AddAttendee(IMeetingsModule meetingsModule, Guid meetingId, int guestsNumber)
        {
            await meetingsModule.ExecuteCommandAsync(new AddMeetingAttendeeCommand(meetingId, guestsNumber));
        }
    }
}