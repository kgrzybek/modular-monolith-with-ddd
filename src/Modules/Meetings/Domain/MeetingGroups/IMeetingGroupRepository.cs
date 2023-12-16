namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups
{
    public interface IMeetingGroupRepository
    {
        Task AddAsync(MeetingGroup meetingGroup);

        Task<int> Commit();

        Task<MeetingGroup> GetByIdAsync(MeetingGroupId id);
    }
}