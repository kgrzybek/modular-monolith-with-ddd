using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting;

namespace CompanyName.MyMeetings.SUT.Helpers
{
    internal static class TestMeetingFactory
    {
        internal static async Task<Guid> GivenMeeting(
            IMeetingsModule meetingsModule,
            Guid meetingGroupId,
            string title,
            DateTime termStartDate,
            DateTime termEndDate,
            string description,
            string meetingLocationName,
            string meetingLocationAddress,
            string meetingLocationPostalCode,
            string meetingLocationCity,
            int? attendeesLimit,
            int guestsLimit,
            DateTime? rsvpTermStartDate,
            DateTime? rsvpTermEndDate,
            decimal? eventFeeValue,
            string eventFeeCurrency,
            List<Guid> hostMemberIds)
        {
            return await meetingsModule.ExecuteCommandAsync(new CreateMeetingCommand(
                meetingGroupId,
                title,
                termStartDate,
                termEndDate,
                description,
                meetingLocationName,
                meetingLocationAddress,
                meetingLocationPostalCode,
                meetingLocationCity,
                attendeesLimit,
                guestsLimit,
                rsvpTermStartDate,
                rsvpTermEndDate,
                eventFeeValue,
                eventFeeCurrency,
                hostMemberIds));
        }
    }
}