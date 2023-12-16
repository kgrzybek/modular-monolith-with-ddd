namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies
{
    public static class MeetingGroupExpirationDatePolicy
    {
        public static List<MeetingGroupId> GetMeetingGroupsCoveredByMemberSubscription(
            List<MeetingGroupMemberData> meetingGroups)
        {
            return meetingGroups
                .Where(x => x.Role == MeetingGroupMemberRole.Organizer)
                .Select(x => x.MeetingGroupId)
                .ToList();
        }
    }
}